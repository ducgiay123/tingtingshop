import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchAllProductsAsync,
  selectProducts,
  selectError,
  selectLoading,
  sortProductsByPriceAsc,
  sortProductsByPriceDesc,
} from "../../redux/slices/productSlice";
import { useNavigate } from "react-router-dom";
import TablePagination from "@mui/material/TablePagination";
import { Input, Button } from "antd";
import {
  SearchOutlined,
  ArrowUpOutlined,
  ArrowDownOutlined,
} from "@ant-design/icons";
import { Spin } from "antd";

import { AppDispatch } from "../../redux/store";
import ProductItem from "./ProductItem";
import DropdownComponent from "../../components/DropdownComponent";
import LoadingComponent from "../../components/LoadingComponent";
import "./productList.css"; // Import the CSS file

const ProductList = () => {
  const dispatch: AppDispatch = useDispatch();
  const allProducts = useSelector(selectProducts);
  const loading = useSelector(selectLoading);
  const error = useSelector(selectError);
  const [page, setPage] = useState(0);
  const [itemsPerPage] = useState(12); // Number of products per page
  const [searchTerm, setSearchTerm] = useState(""); // State for search term
  const [filteredProducts, setFilteredProducts] = useState(allProducts);
  const [isSort, setIsSort] = useState(false);

  const handleSort = (value: boolean) => {
    setIsSort(value);
    if (isSort) {
      dispatch(sortProductsByPriceAsc());
    } else {
      dispatch(sortProductsByPriceDesc());
    }
  };

  useEffect(() => {
    dispatch(
      fetchAllProductsAsync({
        offset: page * itemsPerPage,
        limit: itemsPerPage,
      })
    );
  }, [dispatch, page, itemsPerPage]);

  useEffect(() => {
    // Filter products based on search term
    const filtered = allProducts.filter((product) =>
      product.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredProducts(filtered);
  }, [allProducts, searchTerm]);

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  if (loading) {
    return (
      // <LoadingComponent/>
      <div className="waitingContainer">
        <Spin tip="Loading" size="large">
          <div className="content" />
        </Spin>
      </div>
    );
  }

  if (error) {
    return <div className="waitingContainer">Error: {error}</div>;
  }

  return (
    <div className="productContainer">
      <h1 className="categoryName">All</h1>
      <div className="aboveList">
        <DropdownComponent
          Component={Button}
          className="categoryList"
          label="Categoris"
        />
        <TablePagination
          style={{ color: "#5c3551" }}
          component="div"
          count={100} // Assuming you have the total count of products available
          page={page}
          onPageChange={handleChangePage}
          rowsPerPage={itemsPerPage}
          rowsPerPageOptions={[itemsPerPage]}
        />
        <div>
          <ArrowUpOutlined
            // style={{ fontSize: 20 }}
            onClick={() => handleSort(false)}
            className="sort-icon"
          />
          <ArrowDownOutlined
            // style={{ fontSize: 20 }}
            onClick={() => handleSort(true)}
            className="sort-icon"
          />
          <Input
            size="large"
            placeholder="Search"
            style={{ width: "200px" }}
            prefix={<SearchOutlined />}
            onChange={(e) => setSearchTerm(e.target.value)} // Update search term on change
          />
        </div>
      </div>

      <div className="productListContainer">
        {filteredProducts.map((product) => (
          <ProductItem
            key={product.id}
            product={product}
            className="productItem"
            // onClick={() => handleProductClick(product.id.toString())}
          />
        ))}
      </div>
    </div>
  );
};

export default ProductList;
