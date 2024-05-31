import React, { useState, useEffect } from "react";
import axios from "axios";
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

import { Address, Order, OrderItem } from "../../mics/type";
import { DOMAIN_URL } from "../../asserts/constant";
import "./adminPage.css";
// Define types

const AdminPage: React.FC = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [open, setOpen] = useState<{ [key: string]: boolean }>({});

  const handleRowClick = (id: string) => {
    setOpen({ ...open, [id]: !open[id] });
  };
  useEffect(() => {
    const fetchOrders = async () => {
      try {
        // Add your authorization token here
        let token = localStorage.getItem("token");
        if (token !== null) {
          if (token.startsWith('"') && token.endsWith('"')) {
            token = token.slice(1, -1);
          }
        }

        const response = await axios.get<Order[]>(
          DOMAIN_URL + "orders/getall",
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setOrders(
          response.data.map((order) => ({
            ...order,
            orderDate: new Date(order.orderDate), // Parse the orderDate string into a Date object
          }))
        );
      } catch (error) {
        console.error("Error fetching orders:", error);
      }
    };

    fetchOrders();
  }, []);

  return (
    <div>
      <div className="adminPageContainer">
        <h1>Admin Page</h1>
        <h1>List Orderd </h1>
        <TableContainer component={Paper} style={{ paddingBottom: 400 }}>
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
                      <Collapse in={open[row.id]} timeout="auto" unmountOnExit>
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
  );
};

export default AdminPage;
