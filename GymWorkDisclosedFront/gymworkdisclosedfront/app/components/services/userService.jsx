export async function getUser(email, idToken){

    const response = await fetch(`http://localhost:5206/api/Auth/${email}`, {
        
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${idToken}`
        },
    });
    const data = await response.json();
    return data;
}