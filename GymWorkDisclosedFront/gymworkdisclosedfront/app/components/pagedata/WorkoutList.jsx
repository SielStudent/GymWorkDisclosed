import { getGymGoer } from '../services/gymGoerService'
export default async function GymGoer(filter) {
    console.log("filter", filter)
    console.log("user in workoutlists", filter.filter[2]);
    const gymgoer = await getGymGoer(filter);
    console.log("gymgoer", gymgoer);
    return (
        <div>
            {/*<Filterbar gymgoer={gymgoer} />*/}
            {gymgoer.workouts.map(workout => (
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
        </div>
    )
}


