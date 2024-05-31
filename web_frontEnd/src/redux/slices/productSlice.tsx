import {
  createAsyncThunk,
  createSlice,
  createSelector,
} from "@reduxjs/toolkit";
import axios from "axios";
import { Product, Pagination, Category, UpdateProduct } from "../../mics/type";
import { RootState } from "../store";
import { DOMAIN_URL } from "../../asserts/constant";
const productsUrl = `${DOMAIN_URL}products/`;
const categoriesUrl = `${DOMAIN_URL}categories/`;

interface ProductState {
  data: Product[];
  loading: boolean;
  error: string | null;
  categories: Category[];
}
interface ProductUpdate {
  id: string;
  updatedProduct: UpdateProduct;
}
const initialState: ProductState = {
  data: [],
  loading: false,
  error: null,
  categories: [],
};

export const fetchAllProductsAsync = createAsyncThunk(
  "products/fetchAllProducts",
  async ({ offset, limit }: Pagination, { rejectWithValue }) => {
    try {
      const response = await axios.get<Product[]>(
        `${productsUrl}?offset=${offset}&limit=${limit}`
      );
      return response.data;
    } catch (error: any) {
      throw Error(error.message);
      return rejectWithValue(error);
    }
  }
);

export const fetchCategoriesAsync = createAsyncThunk(
  "products/fetchCategories",
  async () => {
    try {
      const response = await axios.get<Category[]>(categoriesUrl);
      return response.data;
    } catch (error: any) {
      throw Error(error.message);
    }
  }
);
export const updateProductByIdApi = createAsyncThunk(
  "products/updateProductById",
  async ({ id, updatedProduct }: ProductUpdate) => {
    let token = localStorage.getItem("token");
    if (token !== null) {
      if (token.startsWith('"') && token.endsWith('"')) {
        token = token.slice(1, -1);
      }
    }
    const headers = {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json", // Include Content-Type header
    };
    try {
      const response = await axios.put<Product>(
        `${productsUrl}${id}`,
        updatedProduct,
        { headers: headers }
      );
      return response.data;
    } catch (error: any) {
      throw Error(error.message);
    }
  }
);
export const fetchProductsByCategory = createAsyncThunk(
  "products/fetchProductsByCategory",
  async (categoryId: number) => {
    try {
      const response = await axios.get<Product[]>(
        `${productsUrl}?Category_Id=${categoryId}`
      );
      return response.data;
    } catch (error: any) {
      throw Error(error.message);
    }
  }
);

export const productSlice = createSlice({
  name: "products",
  initialState,
  reducers: {
    sortProductsByPriceAsc: (state) => {
      state.data.sort((a, b) => a.price - b.price);
    },
    sortProductsByPriceDesc: (state) => {
      state.data.sort((a, b) => b.price - a.price);
    },
    updateProductByIdSuccessReducer: (state, action) => {
      const index = state.data.findIndex(
        (product) => product.id === action.payload.id
      );
      if (index !== -1) {
        state.data[index] = action.payload;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllProductsAsync.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAllProductsAsync.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(fetchAllProductsAsync.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message ?? "Error fetching data";
      })
      .addCase(fetchCategoriesAsync.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategoriesAsync.fulfilled, (state, action) => {
        state.loading = false;
        state.categories = action.payload;
      })
      .addCase(fetchCategoriesAsync.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message ?? "Error fetching categories";
      })
      .addCase(fetchProductsByCategory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchProductsByCategory.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(fetchProductsByCategory.rejected, (state, action) => {
        state.loading = false;
        state.error =
          action.error.message ?? "Error fetching products by category";
      });
  },
});

export const {
  sortProductsByPriceAsc,
  sortProductsByPriceDesc,
  updateProductByIdSuccessReducer,
} = productSlice.actions;

export const selectProducts = (state: { products: ProductState }) =>
  state.products.data;
export const selectCategories = (state: { products: ProductState }) =>
  state.products.categories;
export const selectLoading = (state: { products: ProductState }) =>
  state.products.loading;
export const selectError = (state: { products: ProductState }) =>
  state.products.error;
export const selectProductById =
  (productId: string | undefined) => (state: RootState) =>
    productId
      ? state.products.data.find(
          (product) => product.id.toString() === productId
        )
      : undefined;

export const selectCategoryByName = (categoryName: string | undefined) =>
  createSelector(
    (state: RootState) => state.products.categories,
    (categories: Category[]) => {
      if (categoryName === undefined) {
        return undefined;
      }
      const category = categories.find(
        (category) => category.name === categoryName
      );
      return category ? category.id : undefined;
    }
  );
export const selectCategoryByID = (categoryID: number) =>
  createSelector(
    (state: RootState) => state.products.categories,
    (categories) => {
      const category = categories.find(
        (category) => category.id === categoryID
      );
      return category;
    }
  );
export default productSlice.reducer;
