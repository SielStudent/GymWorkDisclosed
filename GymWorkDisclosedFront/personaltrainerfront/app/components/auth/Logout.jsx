"use client";
import {getAuth, signOut} from "firebase/auth";
import { auth } from "../firebase/firebase.config";

const Logout = async () => {
  await signOut(auth);
};

export default Logout;