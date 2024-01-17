'use client';
import {signInWithPopup, GoogleAuthProvider} from "firebase/auth";
import {auth} from "../firebase/firebase.config";
const provider = new GoogleAuthProvider();


const Login = async () => {

    await signInWithPopup(auth, provider);
}

export default Login;