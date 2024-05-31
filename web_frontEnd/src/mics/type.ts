export type Product = {
  id: string;
  name: string;
  price: number;
  description: string;
  categoryId: number;
  category: Category;
  images: string[];
};
export type Category = {
  id: number;
  name: string;
  image: string;
};
export type Pagination = {
  offset: number;
  limit: number;
};
export type UserRegister = {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phone: string;
  avatarLink: string;
  role: string;
};

export type User = UserRegister & {
  role: "Customer" | "Admin";
  id: number;
};
export type ProductCart = Product & {
  quantity: number;
  isInCart: boolean;
};
export type ProductSubmit = {
  name: string;
  price: number;
  description: string;
  categoryId: number;
  images: string[];
};
export type UpdateProduct = {
  name: string;
  price: number;
  description: string;
  categoryId: number;
};
export type Address = {
  id: string;
  streetName: string;
  streetNumber: string;
  unitNumber: string;
  postalCode: string;
  city: string;
};
export type Order = {
  id: string;
  orderDate: Date;
  address: Address;
  orderItems: [OrderItem];
};
export type OrderItem = {
  productId: string;
  quantity: number;
  price: number;
};
