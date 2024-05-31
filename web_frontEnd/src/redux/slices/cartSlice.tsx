import { ProductCart, Product } from "../../mics/type";
import { createSlice, createSelector } from "@reduxjs/toolkit";
import { RootState } from "../store";
type InitialState = {
  carts: ProductCart[];
};

const initialState: InitialState = {
  carts: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addItemToCart: (state, action) => {
      const newItem: ProductCart = action.payload;
      const existingItem = state.carts.find((item) => item.id === newItem.id);
      if (!existingItem) {
        state.carts.push({
          ...newItem,
          quantity: newItem.quantity || 1,
          isInCart: true,
        });
      }
    },
    removeItemToCart: (state, action) => {
      state.carts = state.carts.filter((item) => item.id !== action.payload.id);
    },
    updateQuantity: (state, action) => {
      const { id, quantity } = action.payload;
      const itemIndex = state.carts.findIndex((item) => item.id === id);
      if (itemIndex !== -1) {
        state.carts[itemIndex].quantity = quantity;
      }
    },
    clearCart: (state) => {
      state.carts = [];
    },
  },
});

export const { addItemToCart, removeItemToCart, updateQuantity, clearCart } =
  cartSlice.actions;

export const selectCartItems = (state: RootState) => state.cart.carts;

export const selectProductCartById = (productId: string) =>
  createSelector(
    (state: RootState) => state.cart.carts,
    (carts) => carts.find((item) => item.id === productId)
  );
export default cartSlice.reducer;
