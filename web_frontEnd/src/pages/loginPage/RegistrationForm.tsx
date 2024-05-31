import React, { useState } from "react";
import { Form, Input, Select, Button, message, Modal } from "antd";
import { useDispatch } from "react-redux";
import { UserRegister } from "../../mics/type";
import "./registrationForm.css";
import { PlusOutlined, UploadOutlined } from "@ant-design/icons";
import { Upload } from "antd";
import { AppDispatch } from "../../redux/store";
import type { GetProp, UploadFile, UploadProps } from "antd";
import { DOMAIN_URL } from "../../asserts/constant";
import axios from "axios";
type FileType = Parameters<GetProp<UploadProps, "beforeUpload">>[0];

const getBase64 = (file: FileType): Promise<string> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result as string);
    reader.onerror = (error) => reject(error);
  });

const { Option } = Select;

const formItemLayout = {
  labelCol: {
    xs: {
      span: 24,
    },
    sm: {
      span: 8,
    },
  },
  wrapperCol: {
    xs: {
      span: 24,
    },
    sm: {
      span: 16,
    },
  },
};
const prefixSelector = (
  <Form.Item name="prefix" noStyle>
    <Select style={{ width: 70 }}>
      <Option value="86">+358</Option>
      <Option value="87">+87</Option>
    </Select>
  </Form.Item>
);
interface Props {
  visible: boolean;
  setVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const RegistrationForm: React.FC<Props> = ({ visible, setVisible }) => {
  const dispatch: AppDispatch = useDispatch();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false); // State to manage loading state
  const [error, setError] = useState<string | null>(null); // State to manage error state
  const [userInformation, setUserInformation] = useState<UserRegister>({
    firstName: "",
    lastName: "",
    phone: "",
    email: "",
    password: "",
    avatarLink: "",
    role: "",
  });

  const [upload, setUpload] = useState({ file: {}, fileList: [] });

  const showSuccessAlert = () => {
    message.success("Account created successfully!");
  };
  const showFailedAlert = () => {
    message.error("Failed to create account!");
  };
  const uploadFile = async (file: File): Promise<any> => {
    try {
      // console.log("begin", file.name);
      const formData = new FormData();
      formData.append("Image", file);

      const response: any = await axios.post(DOMAIN_URL + "image", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      // console.log("End response:", response.data);
      return response.data.url;
    } catch (error) {
      console.error("Error uploading file:", error);
      throw error;
    }
  };
  const onChangUpload = (response: any) => {
    // console.log(response.images, "res");
    setUpload(response.images);
    // console.log(upload, "up");
  };
  const onFinish = async (e: any) => {
    setLoading(true);
    setError(null);

    const imageLinkRes = uploadFile(e.images.fileList[0].originFileObj);

    userInformation.avatarLink = await Promise.resolve(imageLinkRes);

    try {
      console.log(userInformation, "User");
      const response = await axios.post(
        DOMAIN_URL + "auth/register",
        userInformation
      );

      if (response.status === 200) {
        showSuccessAlert();
        setVisible(false);
      } else {
      }
    } catch (error: any) {
      showFailedAlert();
      console.error("Error:", error);
      setError(error.response?.data?.message || "An error occurred");
    } finally {
      setLoading(false);
    }
  };
  const onChange = (key: string, value: string) => {
    // console.log(key, value);
    setUserInformation((prev) => ({
      ...prev,
      [key]: value,
    }));
  };
  const validateMessages = {
    required: "${label} is required!",
    types: {
      email: "${label} is not a valid email!",
      number: "${label} is not a valid number!",
    },
    number: {
      range: "${label} must be between ${min} and ${max}",
    },
    string: {
      min: "${label} must be at least ${min} characters",
    },
  };
  return (
    <Modal
      open={visible}
      okText="Create"
      cancelText="Cancel"
      onOk={form.submit}
      onCancel={() => setVisible(false)}
    >
      <Form
        {...formItemLayout}
        form={form}
        name="register"
        onFinish={onFinish}
        validateMessages={validateMessages}
      >
        <Form.Item
          name="email"
          label="E-mail"
          rules={[
            {
              type: "email",
              message: "The input is not valid E-mail!",
            },
            {
              required: true,
              message: "Please input your E-mail!",
            },
          ]}
        >
          <Input onChange={(e) => onChange("email", e.target.value)} />
        </Form.Item>
        <Form.Item
          name="password"
          label="Password"
          rules={[
            {
              required: true,
              min: 4,
            },
          ]}
          hasFeedback
        >
          <Input.Password
            onChange={(e) => onChange("password", e.target.value)}
          />
        </Form.Item>

        <Form.Item
          name="firstName"
          label="First Name"
          tooltip="What do you want others to call you?"
          rules={[
            {
              required: true,
              message: "Please input your First Name!",
              whitespace: true,
            },
          ]}
        >
          <Input onChange={(e) => onChange("firstName", e.target.value)} />
        </Form.Item>

        <Form.Item
          name="lastName"
          label="Last Name"
          tooltip="What do you want others to call you?"
          rules={[
            {
              required: true,
              message: "Please input your Last Name!",
              whitespace: true,
            },
          ]}
        >
          <Input onChange={(e) => onChange("lastName", e.target.value)} />
        </Form.Item>
        <Form.Item
          name="Phone"
          label="Phone"
          tooltip="What is your phone?"
          rules={[
            {
              required: true,
              message: "Please input your Phone!",
              whitespace: true,
            },
          ]}
        >
          {/* <Input onChange={(e) => onChange("phone", e.target.value)} /> */}
          <Input
            addonBefore={prefixSelector}
            onChange={(e) => onChange("phone", e.target.value)}
          />
        </Form.Item>
        <Form.Item name="images" label="Images">
          <Upload
            listType="picture-card"
            showUploadList={true}
            beforeUpload={(file) => false}
            maxCount={1}
            onChange={(value) => onChangUpload(value)}
          >
            <UploadOutlined />
          </Upload>
        </Form.Item>
        {/* <Form.Item
          name="avatar"
          label="Avatar"
          tooltip="Link your image?"
          rules={[
            {
              required: true,
              message: "Please input your avatar!",
              whitespace: true,
            },
          ]}
        >
          <Input onChange={(e) => onChange("avatar", e.target.value)} />
        </Form.Item> */}
        {/* <Form.Item
          name="avatar"
          label="Avatar"
          rules={[
            {
              required: true,
            },
          ]}
        >
          <Upload
            listType="picture-circle"
            fileList={fileList}
            // onPreview={handlePreview}
            onChange={handleChange}
          >
            {fileList.length >= 1 ? null : uploadButton}
          </Upload>
        </Form.Item> */}
      </Form>
      {/* <ToastContainer /> */}
    </Modal>
  );
};

export default RegistrationForm;
