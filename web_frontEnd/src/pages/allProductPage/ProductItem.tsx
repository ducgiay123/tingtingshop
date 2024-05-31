import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useSelector } from "react-redux";

import { Product } from "../../mics/type";
import ButtonAddToCart from "../../components/ButtonAddToCart";
import { selectUser } from "../../redux/slices/userSlice";
import AdminButton from "../../components/AdminButton";

import "./productItem.css";

type ProductItemProps = {
  product: Product;
  className: string;
};

const ProductItem: React.FC<ProductItemProps> = ({ product, className }) => {
  const user = useSelector(selectUser);
  const [imgError, setImgError] = useState(false);

  const handleImageError = () => {
    console.log("imgError");
    setImgError(true);
  };

  let isAdmin;
  if (user != undefined) {
    isAdmin = user.role === "Admin";
  }

  const navigate = useNavigate();
  const handleProductClick = (productId: string) => {
    navigate(`/products/${productId}`);
  };

  // const fImage: string[] = product.images.map((img: string) =>
  //   img.replace(/["\[\]]/g, "")
  // );

  return (
    <li className={className}>
      <div className="imgContainer">
        <img
          src={product.images[0]}
          alt={product.name}
          className="test"
          onClick={() => handleProductClick(product.id)}
          onError={() => handleImageError}
        />
        {imgError && <span className="error-message">Invalid Image</span>}
      </div>
      {isAdmin ? (
        <AdminButton product={product} />
      ) : // <div className="adminSection">
      //   <EditOutlined className="adminSectionIcon" />
      //   <DeleteOutlined className="adminSectionIcon" />
      // </div>
      null}

      <p>{product.name}</p>
      <span className="priceProduct">€{product.price}</span>
      {user?.role === "Customer" ? <ButtonAddToCart product={product} /> : null}
    </li>
  );
};

export default ProductItem;
