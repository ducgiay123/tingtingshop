import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import HomePage from "./pages/homePage/HomePage";
import ProductsList from "./pages/allProductPage/ProductList";
import ProfilePage from "./pages/profilePage/ProfilePage";
import NavBar from "./components/NavBar";
import Footer from "./components/Footer";
import LoginPage from "./pages/loginPage/LoginPage";
import PrivateRoute from "./pages/profilePage/PrivateRoute";
import ProductItemPage from "./pages/productPage/ProductItemPage";
import CartPage from "./pages/cartPage/CartPage";
import ProductByCategory from "./pages/allProductPage/ProductByCategory";
import AdminPage from "./pages/adminPage/AdminPage";
import "./app.css";
function App() {
  const userDataString = localStorage.getItem("user");
  const userData = userDataString ? JSON.parse(userDataString) : null;
  return (
    <div className="mainContainter" data-testid="mainContainter">
      <Router>
        <Routes>
          <Route
            path="/"
            element={
              <>
                <NavBar />
                <HomePage />
                <Footer />
              </>
            }
          />
          <Route
            path="/products"
            element={
              <>
                <NavBar />
                <ProductsList />
                <Footer />
              </>
            }
          />
          <Route
            path="/profile"
            element={
              <>
                <NavBar />
                {userData?.role == "Customer" ? (
                  <PrivateRoute
                    Component={ProfilePage}
                    redirect="/login"
                    isLogin={false}
                  />
                ) : (
                  <PrivateRoute
                    Component={AdminPage}
                    redirect="/login"
                    isLogin={false}
                  />
                )}

                <Footer />
              </>
            }
          />
          <Route
            path="/products/:productId"
            element={
              <>
                <NavBar />
                <ProductItemPage />
                <Footer />
              </>
            }
          />
          <Route
            path="/products/category/:categoryName"
            element={
              <>
                <NavBar />
                <ProductByCategory />
                <Footer />
              </>
            }
          />
          <Route
            path="/carts"
            element={
              <>
                <NavBar />
                <CartPage />
                <Footer />
              </>
            }
          />
          <Route
            path="/login"
            element={
              <PrivateRoute
                Component={LoginPage}
                redirect="/profile"
                isLogin={true}
              />
            }
          />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
