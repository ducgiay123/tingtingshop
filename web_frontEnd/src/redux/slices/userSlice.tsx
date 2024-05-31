import { createSlice } from "@reduxjs/toolkit";
import { User } from "../../mics/type";
import { RootState } from "../store";

interface UserState {
  user: User | null;
  isLogined: boolean;
}

const initialState: UserState = {
  user: null,
  isLogined: false,
};

const usersSlice = createSlice({
  name: "user",
  initialState: {
    user: localStorage.getItem("user") || null,
    isLogined: localStorage.getItem("loggedIn") === "true",
  },
  reducers: {
    saveUserInformation: (state, action) => {
      state.user = action.payload;
      state.isLogined = true;
      localStorage.setItem("user", JSON.stringify(action.payload));
      localStorage.setItem("loggedIn", "true");
    },
    logoutUser: (state) => {
      state.user = null;
      state.isLogined = false;
      localStorage.removeItem("user");
      localStorage.removeItem("loggedIn");
      localStorage.removeItem("token");
    },
  },
});

const userReducer = usersSlice.reducer;
export const { saveUserInformation, logoutUser } = usersSlice.actions;
export default userReducer;

const createEmptyUser = (): User => ({
  id: -100,
  firstName: "",
  lastName: "",
  phone: "",
  email: "",
  password: "",
  avatarLink: "",
  role: "Customer",
});

let cachedUser: User;
let lastUserString: string | null = null;
export const selectUser = (state: RootState): User => {
  const userString = state.users.user;

  if (userString === lastUserString) {
    return cachedUser;
  }

  lastUserString = userString;

  if (!userString) {
    cachedUser = createEmptyUser();
  } else {
    cachedUser =
      typeof userString === "string"
        ? JSON.parse(userString)
        : (cachedUser = userString);
  }

  return cachedUser;
};
export const selectLoggIn = (state: RootState) => state.users.isLogined;
