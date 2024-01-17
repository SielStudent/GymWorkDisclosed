"use client";
import {useEffect} from "react";
import {auth} from "@/app/components/firebase/firebase.config";

const LoggedInUser = () => {
    useEffect(() => {
        auth.onAuthStateChanged((user) => {
            if (user) {
                console.log("user is logged in", user)
                return(
                    <div>
                        Logged in
                    </div>
                )
            } else {
                console.log("user is not logged in")
                return(
                    <div>
                        Not logged in
                    </div>
                )
            }
        })
    }, [])
}
export default LoggedInUser;