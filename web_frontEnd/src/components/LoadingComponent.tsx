import React from "react";
import { Spin } from "antd";
import "./loadingComponent.css";
const LoadingComponent = () => {
  return (
    <div className="waitingContainer">
      <Spin tip="Loading" size="large">
        <div className="content" />
      </Spin>
    </div>
  );
};
export default LoadingComponent;
