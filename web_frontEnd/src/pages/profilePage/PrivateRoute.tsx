import React, { useState } from "react";
import { Navigate } from "react-router-dom";
import { selectLoggIn } from "../../redux/slices/userSlice";
import { useSelector } from "react-redux";

interface PrivateRouteProps {
  Component: React.ComponentType<any>;
  redirect: string;
  isLogin: boolean;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({
  Component,
  redirect,
  isLogin,
}) => {
  const loggedIn = useSelector(selectLoggIn);
  // Your authentication logic goes here...

  return !isLogin ? (
    loggedIn ? (
      <Component />
    ) : (
      <Navigate to={redirect} />
    )
  ) : loggedIn ? (
    <Navigate to={redirect} />
  ) : (
    <Component />
  );
};

export default PrivateRoute;
