export async function AddWorkout(Workout, idToken){

    const WorkoutJSON = JSON.stringify({ TimeInSeconds: Workout.totalTime, gymGoerId: Workout.gymGoerId, exerciseId: Workout.exerciseId, Sets: Workout.Sets})
    const response = await fetch(`http://localhost:5206/api/Workout`, {
        next: { revalidate: 250 },
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${idToken}`
        },
        body: WorkoutJSON
    });
    const data = await response.json();
    return data;
}