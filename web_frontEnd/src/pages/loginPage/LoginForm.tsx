import React, { useState } from "react";
import "./loginForm.css";
import { useNavigate, useLocation } from "react-router-dom";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";

import { Form, Input, Button, Checkbox, type FormProps, message } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";

import RegistrationForm from "./RegistrationForm";
import { GoogleOutlined } from "@ant-design/icons";
import { saveUserInformation } from "../../redux/slices/userSlice";
import { AppDispatch, RootState } from "../../redux/store";
import { DOMAIN_URL } from "../../asserts/constant";
type FieldType = {
  username?: string;
  password?: string;
  remember?: string;
};

export default function LoginForm() {
  const dispatch: AppDispatch = useDispatch();
  const check = useSelector((state: RootState) => state.users.isLogined);
  const navigate = useNavigate();
  const location = useLocation();
  const [isRegistered, setRegistered] = useState(false);
  const [form] = Form.useForm();
  const failedToLogin = () => {
    message.error("Invalid username or password");
  };
  const onFinish = async (values: any) => {
    try {
      const { username, password } = values;
      const userData = { email: username, password };

      // Authenticate user
      const response = await axios.post(DOMAIN_URL + "auth/login", userData);
      console.log("Authentication successful");
      navigate("/");
      localStorage.setItem("token", JSON.stringify(response.data.token));
      const accessToken = response.data.token;

      const responseUser = await axios.get(DOMAIN_URL + "auth/profile", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });

      const userProfile = responseUser.data;
      dispatch(saveUserInformation(userProfile));
    } catch (error: any) {
      if (error.response && error.response.status === 401) {
        failedToLogin();
        console.error("Authentication failed: Invalid username or password");
      } else {
        console.error("Authentication failed:", error.message);
      }
    }
  };
  const onFinishFailed: FormProps<FieldType>["onFinishFailed"] = (
    errorInfo
  ) => {
    console.log("Failed:", errorInfo);
  };
  const formStyle: React.CSSProperties = {
    width: "400px",
    padding: "0px 0px 0px 0px",
  };

  return (
    <Form
      title="Login"
      name="normal_login"
      className="login-form"
      form={form}
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      style={formStyle}
    >
      <Form.Item
        name="username"
        rules={[{ required: true, message: "Please input your Username!" }]}
      >
        <Input
          prefix={<UserOutlined className="site-form-item-icon" />}
          placeholder="asd@gmail.com"
        />
      </Form.Item>
      <Form.Item
        name="password"
        rules={[{ required: true, message: "Please input your Password!" }]}
      >
        <Input.Password
          prefix={<LockOutlined className="site-form-item-icon" />}
          type="password"
          placeholder="Password"
        />
      </Form.Item>
      <Form.Item>
        <Form.Item name="remember" valuePropName="checked" noStyle>
          <Checkbox>Remember me</Checkbox>
        </Form.Item>
        <a
          className="login-form-forgot"
          href=""
          style={{ float: "right", color: "black" }}
        >
          {check == true ? "trung" : ""}
          Forgot password
        </a>
      </Form.Item>
      <Form.Item>
        <Button
          type="primary"
          htmlType="submit"
          className="login-form-button"
          style={{
            backgroundColor: "rgb(208,112,190)",
            background:
              "linear-gradient(90deg, rgba(208,112,190,1) 0%, rgba(141,109,228,1) 68%)",
            color: "#f7f1e4",
            fontWeight: "bold",
          }}
        >
          Sign in
        </Button>
        &nbsp;
        <span
          style={{ color: "#b567ca", cursor: "pointer" }}
          onClick={() => setRegistered(true)}
        >
          register now!
        </span>
        <span style={{ float: "right", cursor: "pointer" }}>
          Login with
          <GoogleOutlined style={{ fontSize: 20 }} />
        </span>
      </Form.Item>
      <RegistrationForm visible={isRegistered} setVisible={setRegistered} />
    </Form>
  );
}
