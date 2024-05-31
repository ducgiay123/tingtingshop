import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import {
  fetchCategoriesAsync,
  selectCategories,
  selectLoading,
  selectError,
} from "../../redux/slices/productSlice";
import { Category } from "../../mics/type";
import { AppDispatch } from "../../redux/store";
import "./homePage.css";
const HomePage: React.FC = () => {
  const categories: Category[] = useSelector(selectCategories);
  const loading: boolean = useSelector(selectLoading);
  const error: string | null = useSelector(selectError);
  const navigate = useNavigate();

  const onClick = (value: string) => {
    navigate(`/products/category/${value}`);
  };

  return (
    <div>
      {loading && <div>Loading...</div>}
      {error && <div>Error: {error}</div>}
      <ul className="homePageContainer">
        {categories.map((category) => (
          <li
            key={category.id}
            className="itemCate"
            onClick={() => onClick(category.name)}
          >
            <img
              src={category.image}
              alt={category.name}
              // style={{ width: "100px", height: "100px" }}
            />
            <p>{category.name}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default HomePage;
