import LoginForm from "./LoginForm";
import "./loginPage.css";
import { Typography } from "antd";

const { Title, Text } = Typography;
const LoginPage = () => {
  return (
    <div className="login-page">
      <div className="colorContainer"></div>
      <div className="loginContainer">
        <div className="leftDiv">
          <div className="leftContent">
            <Title level={1}>Welcome</Title>
            <Text>
              Sign in to explore a wide range of products and enjoy seamless
              shopping experience.
            </Text>
            <Title level={2}>Why Choose Us?</Title>
            <Text>
              Our platform offers high-quality products, competitive prices, and
              excellent customer service. With secure payment options and fast
              delivery, shopping with us is convenient and reliable.
            </Text>
          </div>
        </div>
        <div className="rightDiv">
          <div className="loginForm">
            <Title style={{ color: "#8d6de4" }} level={2}>
              Log in
            </Title>
            <LoginForm />
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
