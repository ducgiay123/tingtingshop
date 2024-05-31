import React, { useState, useEffect } from "react";
import axios from "axios";
import { useSelector, useDispatch } from "react-redux";
import { message } from "antd";
import { DOMAIN_URL } from "../../asserts/constant";
import { RootState } from "../../redux/store";
import CartItem from "./CartItem";
import { selectCartItems, clearCart } from "../../redux/slices/cartSlice";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
} from "@mui/material";
import { Modal, Select } from "antd";

import "./cartPage.css";

const { Option } = Select;


const CartPage: React.FC = () => {
  const cartItems = useSelector((state: RootState) => selectCartItems(state));
  const dispatch = useDispatch();
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [addresses, setAddresses] = useState([]);
  const [selectedAddress, setSelectedAddress] = useState("");
  // const currentDate = new Date();

  // const submitOrder = {
  //   orderDate: currentDate.toISOString(),
  //   addressId: "",
  //   orderItems: [
  //     {
  //       productId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  //       quantity: 0,
  //       price: 0,
  //     },
  //   ],
  // };
  const showSuccessAlert = () => {
    message.success("Customer has bought those items  successfully!");
  };
  const showFailedAlert = () => {
    message.error("Failed to order!");
  };
  useEffect(() => {
    const fetchAddresses = async () => {
      try {
        const token = localStorage.getItem("token")?.replace(/"/g, "");
        if (!token) {
          message.error("No token found");
          return;
        }
        const headers = { Authorization: `Bearer ${token}` };
        const response = await axios.get(DOMAIN_URL + "addresses", {
          headers: headers,
        });
        setAddresses(response.data);
      } catch (error) {
        message.error("Failed to fetch addresses");
      }
    };

    fetchAddresses();
  }, []);

  const showModal = () => {
    setIsModalVisible(true);
  };
  const handleSubmitOrder = async () => {
    const submitOrder = {
      orderDate: new Date().toISOString(),
      addressId: selectedAddress, // Set the addressId as needed
      orderItems: cartItems.map((item) => ({
        productId: item.id,
        quantity: item.quantity, // Include the quantity from the cart item
        price: item.price,
      })),
    };

    try {
      const token = localStorage.getItem("token")?.replace(/"/g, "");
      if (!token) {
        message.error("No token found");
        return;
      }
      const response = await axios.post(
        "http://localhost:5192/api/v1/orders",
        submitOrder,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );

      showSuccessAlert();
      handleClearCart();
      // Handle successful response here, e.g., show a success message
      console.log("Order submitted successfully!");
    } catch (error: any) {
      // Handle errors, e.g., show an error message
      showFailedAlert();
      console.error("Error submitting order:", error.message);
    }
  };
  const handleOk = () => {
    handleSubmitOrder();

    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const handleAddressChange = (value: string) => {
    setSelectedAddress(value);
  };

  let totalPrice = 0;
  cartItems.map((item) => {
    totalPrice += item.quantity * item.price;
  });

  const handleClearCart = () => {
    dispatch(clearCart());
  };

  return (
    <div className="cartPage">
      <h1>Cart List</h1>
      {cartItems.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <>
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell></TableCell>
                  <TableCell></TableCell>
                  <TableCell></TableCell>
                  <TableCell></TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="secondary"
                      onClick={handleClearCart}
                      style={
                        {
                          // marginTop: "10px",
                          // float: "right",
                          // right: "10px",
                        }
                      }
                    >
                      Clear Cart
                    </Button>
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell style={{ fontSize: 20, fontWeight: "bold" }}>
                    Image
                  </TableCell>
                  <TableCell style={{ fontSize: 20, fontWeight: "bold" }}>
                    Name
                  </TableCell>
                  <TableCell style={{ fontSize: 20, fontWeight: "bold" }}>
                    Quantity
                  </TableCell>
                  <TableCell style={{ fontSize: 20, fontWeight: "bold" }}>
                    Price
                  </TableCell>
                  <TableCell style={{ fontSize: 20, fontWeight: "bold" }}>
                    Action
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {cartItems.map((item) => (
                  <CartItem key={item.id} product={item} />
                ))}
                <TableRow>
                  <TableCell></TableCell>
                  <TableCell></TableCell>
                  <TableCell></TableCell>
                  <TableCell>€{totalPrice}</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="secondary"
                      onClick={showModal}
                      style={{
                        color: "white",
                        backgroundColor: "#5c3551", // Custom color
                      }}
                    >
                      Check Out
                    </Button>
                    <Modal
                      title="Confirm Checkout"
                      open={isModalVisible}
                      onCancel={handleCancel}
                      onOk={handleOk}
                    >
                      <p>Are you sure you want to proceed with the checkout?</p>
                      <Select
                        placeholder="Select your address"
                        onChange={handleAddressChange}
                        style={{ width: "100%" }}
                      >
                        {addresses.map((address: any) => (
                          <Option key={address.id} value={address.id}>
                            {address.streetName},{address.streetNumber},{" "}
                            {address.streetName},{address.unitNumber},
                            {address.city}, {address.postalCode}
                          </Option>
                        ))}
                      </Select>
                    </Modal>
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </TableContainer>
        </>
      )}
    </div>
  );
};

export default CartPage;
