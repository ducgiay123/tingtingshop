import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import axios from "axios";
import { EditText, EditTextarea } from "react-edit-text";
import "react-edit-text/dist/index.css";
import { Button } from "antd";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Collapse,
} from "@mui/material";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";

import { selectUser } from "../../redux/slices/userSlice";
import ProductCreateForm from "./ProductCreateForm";
import "./profilePage.css";
import { OrderItem, Order } from "../../mics/type";



const ProfilePage = () => {
  const user = useSelector(selectUser);
  const [visible, setVisible] = useState(false);
  const [orders, setOrders] = useState<Order[]>([]);
  const [open, setOpen] = useState<{ [key: string]: boolean }>({});

  const handleRowClick = (id: string) => {
    setOpen({ ...open, [id]: !open[id] });
  };

  const showModal = () => {
    setVisible(true);
  };

  const handleCancel = () => {
    setVisible(false);
  };

  const onFinish = (values: any) => {
    console.log("Received values:", values);
    setVisible(false); // Close modal after form submission
  };
  useEffect(() => {
    const fetchOrders = async () => {
      try {
        let token = localStorage.getItem("token");
        if (token !== null) {
          if (token.startsWith('"') && token.endsWith('"')) {
            token = token.slice(1, -1);
          }
        }

        const response = await axios.get<Order[]>(
          "http://localhost:5192/api/v1/orders",
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setOrders(response.data);
      } catch (error) {
        console.error("Error fetching orders:", error);
      }
    };

    fetchOrders();
  }, []);
  return (
    <div>
      {user.id === -100 ? (
        <div className="waitingContainer">Something wrong</div>
      ) : (
        <div className="profileContainer">
          <div className="profileMain">
            <img
              src={user.avatarLink}
              alt="Avatar"
              style={{ height: "40%", width: "40%", borderRadius: "50%" }}
            />
            <div className="profileInfo">
              <div className="profileSection">
                <p>Name:</p>
                <EditText
                  inline={true}
                  showEditButton
                  value={user.firstName + " " + user.lastName}
                  style={{ fontSize: 20 }}
                />
              </div>

              <div className="profileSection">
                <p>Email:</p>
                <EditText
                  inline={true}
                  showEditButton
                  value={user.email}
                  style={{ fontSize: 20 }}
                />
              </div>
              <div className="profileSection">
                <p>Phone: </p>
                <EditText
                  inline={true}
                  showEditButton
                  value={user.phone}
                  style={{ fontSize: 20 }}
                />
              </div>
              <div className="profileSection">
                <p>Role: {user.role}</p>
              </div>
              <Button
                type="primary"
                block
                size="large"
                style={{
                  backgroundColor: "#5c3551",
                  color: "wthie",
                  marginBottom: "15px",
                }}
              >
                Update Profile
              </Button>
              {user.role === "Admin" && (
                <Button
                  type="primary"
                  block
                  size="large"
                  onClick={showModal}
                  style={{
                    backgroundColor: "#5c3551",
                    color: "wthie",
                  }}
                >
                  Create New Product
                </Button>
              )}
              <ProductCreateForm
                visible={visible}
                onCancel={handleCancel}
                onFinish={onFinish}
              />
            </div>
          </div>
          <div className="oderTableContainer">
            <h1>List Orderd </h1>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell />
                    <TableCell>ID</TableCell>
                    <TableCell>OrderDate</TableCell>
                    <TableCell>Address</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {orders.map((row) => (
                    <React.Fragment key={row.id}>
                      <TableRow onClick={() => handleRowClick(row.id)}>
                        <TableCell>
                          {open[row.id] ? (
                            <KeyboardArrowUpIcon />
                          ) : (
                            <KeyboardArrowDownIcon />
                          )}
                        </TableCell>
                        <TableCell>{row.id}</TableCell>
                        <TableCell>{row.orderDate.toString()}</TableCell>
                        <TableCell>{row.address.streetName}</TableCell>
                      </TableRow>
                      <TableRow>
                        <TableCell
                          style={{ paddingBottom: 0, paddingTop: 0 }}
                          colSpan={3}
                        >
                          <Collapse
                            in={open[row.id]}
                            timeout="auto"
                            unmountOnExit
                          >
                            <Table size="small" aria-label="purchases">
                              <TableBody>
                                {row.orderItems.map((item) => (
                                  <TableRow key={item.productId}>
                                    <TableCell>{item.productId}</TableCell>
                                    <TableCell>{item.quantity}</TableCell>
                                    <TableCell>{item.price}</TableCell>
                                  </TableRow>
                                ))}
                              </TableBody>
                            </Table>
                          </Collapse>
                        </TableCell>
                      </TableRow>
                    </React.Fragment>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </div>
        </div>
      )}
    </div>
  );
};

export default ProfilePage;
