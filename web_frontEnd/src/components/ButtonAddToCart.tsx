import React from "react";
import { CheckOutlined } from "@ant-design/icons";
import { Button } from "antd";

import { Product } from "../mics/type";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch } from "../redux/store";
import {
  addItemToCart,
  selectProductCartById,
} from "../redux/slices/cartSlice";

interface ButtonAddToCartProps {
  product: Product;
}

const ButtonAddToCart: React.FC<ButtonAddToCartProps> = ({ product }) => {
  const dispatch: AppDispatch = useDispatch();
  const selectProducCart = useSelector(selectProductCartById(product.id));
  const buttonText = selectProducCart ? <CheckOutlined /> : "Add to Cart";

  const handleAddToCart = (value: Product) => {
    dispatch(addItemToCart(value));
  };
  return (
    <Button
      onClick={() => handleAddToCart(product)}
      type="primary"
      block
      style={{
        backgroundColor: "#5c3551",
        color: "white",
        marginTop: "10px",
      }}
    >
      {buttonText}
    </Button>
  );
};

export default ButtonAddToCart;
