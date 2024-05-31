import React, { useState } from "react";
import { Modal, Button, Form, Input, Select, message, Upload } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import { useSelector } from "react-redux";
import axios from "axios";

import { ProductSubmit, Category } from "../../mics/type";
import { selectCategories } from "../../redux/slices/productSlice";
import { DOMAIN_URL } from "../../asserts/constant";
const { TextArea } = Input;
const { Option } = Select;
interface ModalFormProps {
  visible: boolean;
  onCancel: () => void;
  onFinish: (values: any) => void;
}

const ProductCreateForm: React.FC<ModalFormProps> = ({
  visible,
  onCancel,
  onFinish,
}) => {
  const [imageLinks, setImageLinks] = useState<string[]>([]);
  const [form] = Form.useForm();
  const [product, setProduct] = useState<ProductSubmit>({
    name: "",
    price: 0,
    description: "",
    categoryId: 0,
    images: [],
  });

  const carts: Category[] = useSelector(selectCategories);

  const onChange = (key: string, value: any) => {
    setProduct((prev) => ({
      ...prev,
      [key]: value,
    }));
  };
  const showSuccessAlert = () => {
    message.success("Product created successfully!");
  };
  const showFailedAlert = () => {
    message.error("Failed to create new Product!");
  };
  const createNewProduct = async (newProductData: ProductSubmit) => {
    let token = localStorage.getItem("token");
    if (token !== null) {
      if (token.startsWith('"') && token.endsWith('"')) {
        token = token.slice(1, -1);
      }
    }
    const headers = {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json", // Include Content-Type header
    };
    try {
      const response = await axios.post(
        DOMAIN_URL + "products",
        newProductData,
        { headers: headers }
      );
      if (response.status === 201) {
        showSuccessAlert();
      }
      return response.data; // Return the created product data
    } catch (error: any) {
      showFailedAlert();
      console.log(error.message);
      //   throw new Error("Error creating new product: " + error.message);
    }
  };

  const uploadFile = async (file: File): Promise<any> => {
    try {
      console.log("begin", file.name);
      const formData = new FormData();
      formData.append("Image", file);

      const response: any = await axios.post(
        DOMAIN_URL + "image",
        // "https://api.escuelajs.co/api/v1/files/upload",
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      console.log("End response:", response.data);
      return response.data.url;
    } catch (error) {
      console.error("Error uploading file:", error);
      throw error;
    }
  };

  // };
  const onSubmit = async (e: any) => {
    // e.preventDefault();
    // console.log({ e });
    const imageLinkRes = e.images.fileList.map((img: any) => {
      return uploadFile(img.originFileObj);
    });
    product.images = await Promise.all(imageLinkRes);
    console.log(product);
    createNewProduct(product);
  };
  const getImages = (e: any) => {
    console.log("Upload event:", e);

    if (Array.isArray(e)) {
      return e;
    }
    return e && e.fileList;
  };
  return (
    <Modal
      title="Basic Modal"
      open={visible}
      onCancel={onCancel}
      onOk={form.submit}
      footer={[
        <Button key="back" onClick={onCancel} style={{ display: "none" }}>
          Cancel
        </Button>,
        <Button
          key="submit"
          type="primary"
          onClick={onFinish}
          style={{ display: "none" }}
        >
          OK
        </Button>,
      ]}
    >
      <Form
        name="basic"
        initialValues={{ remember: true }}
        form={form}
        onFinish={onSubmit}
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        layout="horizontal"
      >
        <Form.Item
          label="Name"
          name="Name"
          rules={[{ required: true, message: "Please input title!" }]}
        >
          <Input onChange={(e) => onChange("name", e.target.value)} />
        </Form.Item>

        <Form.Item
          label="Price"
          name="price"
          rules={[{ required: true, message: "Please input price!" }]}
        >
          <Input
            type="number"
            onChange={(e) => onChange("price", e.target.value)}
          />
        </Form.Item>
        <Form.Item
          label="Description"
          name="description"
          rules={[{ required: true, message: "Please input decription!" }]}
        >
          <TextArea
            rows={4}
            onChange={(e) => onChange("description", e.target.value)}
          />
        </Form.Item>
        <Form.Item
          label="Category"
          name="category"
          rules={[{ required: true, message: "Please input category!" }]}
        >
          <Select
            placeholder="Select Category"
            onChange={(value) => onChange("categoryId", value)}
          >
            {carts.map((item) => (
              <Option value={item.id}>{item.name}</Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item name="images" label="Images">
          <Upload
            listType="picture-card"
            showUploadList={true}
            beforeUpload={(file) => false}
          >
            <UploadOutlined />
          </Upload>
        </Form.Item>
        {/* <Form.Item
          label="Images"
          name="images"
          getValueFromEvent={getImages}
          rules={[{ required: true, message: "Please input Images!" }]}
        >
          <Upload>
            <Button icon={<UploadOutlined />}>Upload</Button>
          </Upload>
          <Input
            value={newImageLink}
            onChange={(e) => setNewImageLink(e.target.value)}
            addonAfter={<Button onClick={handleAddImageLink}>Add</Button>}
          />
        </Form.Item> */}
        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          {imageLinks.map((link, index) => (
            <div key={index}>{index + 1} image</div>
          ))}
        </Form.Item>
        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default ProductCreateForm;
