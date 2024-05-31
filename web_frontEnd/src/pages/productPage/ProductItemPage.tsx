import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import AliceCarousel from "react-alice-carousel";
import "react-alice-carousel/lib/alice-carousel.css";
import "react-awesome-slider/dist/styles.css";
import "react-slideshow-image/dist/styles.css";

import { DOMAIN_URL } from "../../asserts/constant";
import ButtonAddToCart from "../../components/ButtonAddToCart";
import { Product } from "../../mics/type";
import "./productItemPage.css";
import { selectUser } from "../../redux/slices/userSlice";
import { useSelector } from "react-redux";

const ProductItemPage = () => {
  const { productId } = useParams<{ productId: string }>();
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const user = useSelector(selectUser);
  useEffect(() => {
    const fetchProductById = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${DOMAIN_URL}products/${productId}`);
        if (response.ok) {
          const productData = await response.json();
          setProduct(productData);
        } else {
          throw new Error("Failed to fetch product data");
        }
      } catch (error) {
        console.error("Error fetching product data:", error);
      } finally {
        setLoading(false);
      }
    };

    if (productId) {
      fetchProductById();
    }
  }, [productId]);

  if (loading) {
    return <div className="waitingContainer">Loading...</div>;
  }

  if (!product) {
    return <div className="waitingContainer">Product not found</div>;
  }

  const fImage: string[] = product.images.map((img: string) =>
    img.replace(/["\[\]]/g, "")
  );
  console.log(fImage);
  return (
    <div className="productPage">
      <div className="containerProductPage">
        <div className="slickCover">
          <AliceCarousel
            // autoPlay={true}
            autoPlayInterval={2000}
            infinite={true}
          >
            {product.images.map((item, index) => (
              <img
                className="productImage"
                src={item}
                key={index}
                alt="Image Broken"
              />
              // <div className="each-slide-effect">
              //   <div
              //     className="productImage"
              //     key={index}
              //     style={{ backgroundImage: `url(${item})` }}
              //     // alt={product.title}
              //   />
              // </div>
            ))}
          </AliceCarousel>
        </div>

        <div className="rightProductPage">
          <h1>Product Details</h1>
          <p>Product Name: {product.name}</p>
          <p>Price: {product.price} € </p>
          {/* <p>Category: {product.category.name}</p> */}
          <p>Description: {product.description}</p>
          {user.role === "Customer" ? (
            <ButtonAddToCart product={product} />
          ) : null}
        </div>
      </div>
    </div>
  );
};

export default ProductItemPage;
