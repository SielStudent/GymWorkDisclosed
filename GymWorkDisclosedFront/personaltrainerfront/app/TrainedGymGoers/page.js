"use client"
import {getUser} from "@/app/components/services/userService";
import GymGoers from "@/app/components/pagedata/TrainedGymGoerMapping";
import {auth} from "@/app/components/firebase/firebase.config";

export default async function TrainedGymGoers(){
    const email = auth.currentUser.email;
    const token = await auth.currentUser.getIdToken();
    const user = await getUser(email, token)
    const data = [user, token]
    
    return (
        <main>
            <GymGoers user={data}/>
        </main>
    )
}