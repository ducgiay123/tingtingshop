import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchProductsByCategory,
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
import { selectCategoryByName } from "../../redux/slices/productSlice";
import "./productList.css";

const ProductByCategory = () => {
  const dispatch: AppDispatch = useDispatch();
  const allProducts = useSelector(selectProducts);
  const loading = useSelector(selectLoading);
  const error = useSelector(selectError);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredProducts, setFilteredProducts] = useState(allProducts);
  const params = useParams<{ categoryName?: string }>();
  const categoryName = params.categoryName;
  const categoryId = useSelector(selectCategoryByName(categoryName));
  const [page, setPage] = useState(0);
  const [itemsPerPage] = useState(12);
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
    if (categoryId !== undefined) {
      setPage(0);
      dispatch(fetchProductsByCategory(categoryId));
    }
  }, [dispatch, categoryId]);

  useEffect(() => {
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

  // Calculate start index and end index for pagination
  const startIndex = page * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const paginatedProducts = filteredProducts.slice(startIndex, endIndex);

  return (
    <div className="productContainer">
      <h1 className="categoryName">{categoryName?.toUpperCase()}</h1>
      <div className="aboveList">
        <DropdownComponent
          Component={Button}
          className="categoryList"
          label="Categoris"
        />
        <TablePagination
          style={{ color: "#5c3551" }}
          component="div"
          count={filteredProducts.length}
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
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
      </div>

      <div className="productListContainer">
        {paginatedProducts.map((product) => (
          <ProductItem
            key={product.id}
            product={product}
            className="productItem"
          />
        ))}
      </div>
    </div>
  );
};

export default ProductByCategory;
