import React, { ChangeEvent, useState } from "react";
import { ProductCart } from "../../mics/type"; // Assuming you have defined ProductCart type
import { Button, TableCell, TableRow } from "@mui/material";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../../redux/store";
import { removeItemToCart, updateQuantity } from "../../redux/slices/cartSlice";

interface CartItemProps {
  product: ProductCart;
}

const CartItem: React.FC<CartItemProps> = ({ product }) => {
  const fImage: string[] = product.images.map((img: string) =>
    img.replace(/["\[\]]/g, "")
  );
  const [number, setNumber] = useState(product.quantity);
  const dispatch: AppDispatch = useDispatch();
  const priceOfEach = product.price * product.quantity;

  const handleRemove = () => {
    dispatch(removeItemToCart(product));
  };

  const handleQuantityChange = (event: ChangeEvent<HTMLInputElement>) => {
    const id: string = product.id;
    const quantity: number = parseInt(event.target.value);
    setNumber(quantity); // Update local state
    dispatch(updateQuantity({ id, quantity: quantity })); // Dispatch action to update quantity
  };
  return (
    <TableRow>
      <TableCell>
        <img
          src={fImage[0]}
          alt={product.name}
          style={{ width: "50px", height: "50px" }}
        />
      </TableCell>
      <TableCell>{product.name}</TableCell>
      <TableCell>
        <input
          type="number"
          value={number}
          onChange={handleQuantityChange} // Call handleQuantityChange directly, not in an arrow function
        />
      </TableCell>
      <TableCell>€{priceOfEach}</TableCell>
      <TableCell>
        <Button
          variant="contained"
          color="primary"
          onClick={handleRemove}
          style={{
            width: "110px",
            color: "white",
            backgroundColor: "#5c3551", // Custom color
          }}
        >
          Remove
        </Button>
      </TableCell>
    </TableRow>
  );
};

export default CartItem;
