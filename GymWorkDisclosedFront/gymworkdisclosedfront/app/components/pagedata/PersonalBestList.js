import {getPersonalBests} from "../services/workoutService";

export default async function PersonalBestList(data) {
    console.log("usertest", data)
    const PersonalBests = await getPersonalBests(data)
    console.log(PersonalBests)
    return (
        <div>
            {PersonalBests.exercises.map(personalbest => (
                <div key={personalbest.id}>
                    <h1>Best {personalbest.name} workouts</h1>
                    <h3>Best Time: {personalbest.bestTimeWorkout.timeInSeconds} seconds</h3>
                    <h3>Most Weight: {personalbest.bestWeightWorkout.maxWeight}</h3>
                    <h3>Total Reps: {personalbest.bestRepsWorkout.totalReps}</h3>
                </div>
            ))}
        </div>
    )
}