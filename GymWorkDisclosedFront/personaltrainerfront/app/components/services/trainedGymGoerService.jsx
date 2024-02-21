export default async function getTrainedGymGoers(user){
    console.log("guid", user.user[0].guid)
    const response = await fetch(`http://localhost:5206/api/PersonalTrainer/${user.user[0].guid}`, {
        method: 'GET',
        headers: {
            'Constant-Type': 'application/json',
            'Authorization': `Bearer ${user.user[1]}}`
        }
    });
    const data = await response.json();
    console.log("data", data)
    return data;
}