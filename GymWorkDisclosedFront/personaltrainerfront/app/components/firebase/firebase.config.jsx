import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";

const firebaseConfig = {
    apiKey: "AIzaSyCLezkXuVWLypX_EmZSNmGCZSz9ALCF1Os",
    authDomain: "gymworkdidsclosedoauth.firebaseapp.com",
    projectId: "gymworkdidsclosedoauth",
    storageBucket: "gymworkdidsclosedoauth.appspot.com",
    messagingSenderId: "230619720980 ",
    appId: "1:230619720980:web:887de9a66bbdd37de8a601",
    measurementId: "G-N394C4YHC1"
};

const app = initializeApp(firebaseConfig);
export const auth = getAuth();