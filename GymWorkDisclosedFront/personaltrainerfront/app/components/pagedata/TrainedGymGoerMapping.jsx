import getTrainedGymGoers from "@/app/components/services/trainedGymGoerService";

export default async function GymGoers(user) {
    const TrainedGymGoers = await getTrainedGymGoers(user);
    console.log("trainer", TrainedGymGoers)
    console.log("trainedGymGoers", TrainedGymGoers.gymGoers)
    return (
        <div>
            {TrainedGymGoers.gymGoers.map(gymgoers => (
                <div key={gymgoers.id}>
                    <h1>{gymgoers.name}</h1>
                    <details>
                    {gymgoers.workouts.map(workout => (
                        <div key={workout.id}>
                            <h1>{workout.exercise.name} workout</h1>
                            <details className='workoutDetails'>
                                <h3>Total time: {workout.timeInSeconds} seconds</h3>
                                <h3>Date of workout: {workout.date}</h3>
                                {workout.sets.map((set, index) => (
                                    <div key={set.id}>
                                        <h1>Set {index + 1}</h1>
                                        <details className='setDetails'>
                                            <h4>Reps: {set.reps}</h4>
                                            <h4>Weight: {set.weight}</h4>
                                            <h4>Time: {set.timeInSeconds} seconds</h4>
                                        </details>
                                    </div>
                                ))}
                            </details>
                        </div>
                    ))}
                    </details>
                </div>
            ))}
        </div>
    )
}