import React, { useState } from "react";
import { Modal, Button, Form, Input, message, Select } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import axios from "axios";

import { Category, Product, ProductSubmit, UpdateProduct } from "../mics/type"; // Assuming you have a Product type defined
import {
  updateProductByIdSuccessReducer,
  updateProductByIdApi,
  selectCategories,
} from "../redux/slices/productSlice";
import { AppDispatch } from "../redux/store";
import "./adminButton.css";
import { useDispatch, useSelector } from "react-redux";
import { DOMAIN_URL } from "../asserts/constant";

const { TextArea } = Input;
const { Option } = Select;
interface AdminButtonProps {
  product: Product;
}
const AdminButton: React.FC<AdminButtonProps> = ({ product }) => {
  const dispatch: AppDispatch = useDispatch();

  const [editModalVisible, setEditModalVisible] = useState(false);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [updatedProduct, setUpdatedProduct] = useState<UpdateProduct>({
    name: product.name,
    price: product.price,
    description: product.description,
    categoryId: product.categoryId,
  });
  const carts: Category[] = useSelector(selectCategories);

  const deleteProduct = async (productId: string) => {
    try {
      const response = await axios.delete(DOMAIN_URL + "products/" + productId);
      if (response.data === true) {
        deleteSuccess();
      }
      return response.data; // Return the deleted product data
    } catch (error: any) {
      deleteError();
      console.log("Failed to delete product: " + error.message);
      //   throw new Error("Failed to delete product: " + error.message);
    }
  };

  const handleEditClick = () => {
    setEditModalVisible(true);
  };

  const handleDeleteClick = () => {
    setDeleteModalVisible(true);
  };

  const handleEditModalCancel = () => {
    setEditModalVisible(false);
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
  };

  const handleDeleteProduct = () => {
    // Logic to delete product
    deleteProduct(product.id);
    window.location.reload();
    handleEditModalCancel();
    // console.log("Delete Product");
  };
  const deleteSuccess = () => {
    message.success("Delete Product Successfully");
  };
  const deleteError = () => {
    message.error("Delete Product Error");
  };

  const onChange = (key: string, value: any) => {
    setUpdatedProduct((prev) => ({
      ...prev,
      [key]: value,
    }));
  };
  const onSubmit = () => {
    console.log(updatedProduct);
    const finalUp: UpdateProduct = {
      name: updatedProduct.name,
      price: updatedProduct.price,
      description: updatedProduct.description,
      categoryId: updatedProduct.categoryId,
    };
    const fuProduct = { id: product.id, updatedProduct };
    dispatch(updateProductByIdSuccessReducer(finalUp));
    dispatch(updateProductByIdApi(fuProduct));
    // form.resetFields();
  };
  return (
    <div className="adminSection">
      <EditOutlined className="adminSectionIcon" onClick={handleEditClick} />
      <DeleteOutlined
        className="adminSectionIcon"
        onClick={handleDeleteClick}
      />
      {/* Edit Modal */}
      <Modal
        title="Edit Item"
        open={editModalVisible}
        onCancel={handleEditModalCancel}
        footer={[
          <Button
            key="cancel"
            onClick={handleEditModalCancel}
            style={{ display: "none" }}
          >
            Cancel
          </Button>,
          <Button
            key="save"
            type="primary"
            onClick={handleEditModalCancel}
            style={{ display: "none" }}
          >
            Save
          </Button>,
        ]}
      >
        <Form
          name="editForm"
          onFinish={onSubmit}
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          initialValues={product} // Set initial values from the product
        >
          <Form.Item
            label="Name"
            name="name"
            // rules={[{ required: true, message: "Please input title!" }]}
          >
            <Input onChange={(e) => onChange("name", e.target.value)} />
          </Form.Item>

          <Form.Item
            label="Price"
            name="price"
            // rules={[{ required: true, message: "Please input price!" }]}
          >
            <Input
              type="number"
              onChange={(e) => onChange("price", e.target.value)}
            />
          </Form.Item>
          <Form.Item
            label="Description"
            name="description"
            // rules={[{ required: true, message: "Please input decription!" }]}
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
          <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
            <Button type="primary" htmlType="submit">
              Update
            </Button>
          </Form.Item>
        </Form>
      </Modal>
      {/* Delete Modal */}
      <Modal
        title="Are You Sure To Delete This Product?"
        open={deleteModalVisible}
        onCancel={handleDeleteModalCancel}
        footer={[
          <Button key="cancel" onClick={handleDeleteModalCancel}>
            Cancel
          </Button>,
          <Button key="delete" type="primary" onClick={handleDeleteProduct}>
            Delete
          </Button>,
        ]}
      >
        {/* Delete Modal Content */}
      </Modal>
    </div>
  );
};

export default AdminButton;
