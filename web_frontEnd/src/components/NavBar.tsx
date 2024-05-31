import React, { useState, useEffect } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import ShoppingBasketIcon from "@mui/icons-material/ShoppingBasket";
import Badge from "@mui/material/Badge";
import { Avatar, Dropdown } from "antd";
import type { MenuProps } from "antd";
import { useDispatch, useSelector } from "react-redux";

import "./navBar.css";
import Logo from "../asserts/logo_transparent.png";
import { AppDispatch } from "../redux/store";
import { logoutUser, selectUser } from "../redux/slices/userSlice";
import { fetchCategoriesAsync } from "../redux/slices/productSlice";
import { selectCartItems } from "../redux/slices/cartSlice";
import DropDownComponent from "./DropdownComponent";

const Navbar: React.FC = () => {
  const location = useLocation();
  const navigator = useNavigate();
  const userData = useSelector(selectUser);
  // const userData = userDataString ? JSON.parse(userDataString) : null;
  const dispatch: AppDispatch = useDispatch();
  const carts = useSelector(selectCartItems);
  const [categoriesFetched, setCategoriesFetched] = useState(false);

  useEffect(() => {
    if (!categoriesFetched) {
      dispatch(fetchCategoriesAsync());
      setCategoriesFetched(true);
    }
  }, [dispatch, categoriesFetched]);

  const onLogout = () => {
    dispatch(logoutUser());
    navigator("/");
    window.location.reload();
  };

  const onGoToCart = () => {
    navigator("/carts");
  };

  const isActive = (path: string) => {
    return location.pathname === path ? "active" : "";
  };

  const items: MenuProps["items"] = [
    {
      key: "1",
      label: <Link to="/profile">Profile</Link>,
    },
    {
      key: "2",
      label: <a onClick={onLogout}>Log Out</a>,
    },
  ];

  return (
    <div className="navbar">
      <div className="left">
        <Link to="/" className={isActive("/")}>
          Home
        </Link>
        <DropDownComponent
          label="Products"
          className={isActive("/product")}
          Component={Link}
        />
      </div>
      <div className="middle">
        <img src={Logo} alt="Logo" className="logo" />
      </div>
      <div className="right">
        {userData?.role == "Customer" && (
          <Badge
            badgeContent={carts.length}
            color="secondary"
            anchorOrigin={{ vertical: "top", horizontal: "left" }}
          >
            <ShoppingBasketIcon
              className="shoppingCartIcon"
              style={{ fontSize: 36, marginRight: 20 }}
              onClick={onGoToCart}
            />
          </Badge>
        )}

        {userData == null ? (
          <Link to="/login">Sign In</Link>
        ) : (
          <Dropdown menu={{ items }} placement="bottomRight">
            <Avatar
              size={64}
              src={userData.avatarLink}
              style={{ cursor: "pointer" }}
              onClick={() => navigator("/profile")}
            />
          </Dropdown>
        )}
      </div>
    </div>
  );
};

export default Navbar;
