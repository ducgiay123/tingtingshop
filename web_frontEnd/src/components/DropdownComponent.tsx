import React from "react";
import type { MenuProps } from "antd";
import { useSelector } from "react-redux";
import { Dropdown, Button } from "antd";
import { Link } from "react-router-dom";

import { Category } from "../mics/type";
import { selectCategories } from "../redux/slices/productSlice";

interface CustomDropdown {
  Component: React.ComponentType<any>;
  label: string;
  className: string;
}

const DropdownComponent: React.FC<CustomDropdown> = ({
  Component,
  label,
  className,
}) => {
  const categories: Category[] = useSelector(selectCategories);
  const items: MenuProps["items"] = categories.map((item) => {
    return {
      key: item.id,
      label: <Link to={`/products/category/${item.name}`}>{item.name}</Link>,
    };
  });
  items.unshift({
    key: -1,
    label: <Link to="/products">ALL</Link>,
  });
  return (
    <Dropdown menu={{ items }} placement="bottomLeft">
      <Component className={className}>{label}</Component>
    </Dropdown>
  );
};

export default DropdownComponent;
