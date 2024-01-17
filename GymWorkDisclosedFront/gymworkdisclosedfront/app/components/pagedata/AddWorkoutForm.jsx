"use client"
import { useState, useEffect } from "react";
import { getExercises } from "../services/exerciseService";
import { AddWorkout } from "../services/addworkout";

// A custom hook to fetch data from the backend api
const useFetch = (url) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getExercises();
        console.log(response)
        setData(response);
        setLoading(false);
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();
  }, [url]);

  return { data, loading };
};

// A component to render a dropdown menu with dynamic data
const Dropdown = ({ data, onChange, value }) => {
  return (
    <select value={value} onChange={onChange}>
      {data.map((item) => (
        <option key={item.guid} value={item.guid}>
          {item.name}
        </option>
      ))}
    </select>
  );
};

// A component to render a set of inputs for reps, weight and timeInSeconds
const Set = ({ index, onChange, values }) => {
  return (
    <div>
      <label htmlFor={`reps-${index}`}>Reps:</label>
      <input
        id={`reps-${index}`}
        type="number"
        name="reps"
        value={values.reps}
        onChange={(e) => onChange(index, e)}
      />
      <label htmlFor={`weight-${index}`}>Weight:</label>
      <input
        id={`weight-${index}`}
        type="number"
        name="weight"
        value={values.weight}
        onChange={(e) => onChange(index, e)}
      />
      <label htmlFor={`timeInSeconds-${index}`}>Time in seconds:</label>
      <input
        id={`timeInSeconds-${index}`}
        type="number"
        name="timeInSeconds"
        value={values.timeInSeconds}
        onChange={(e) => onChange(index, e)}
      />
    </div>
  );
};

// A component to render the form with the inputs and the dropdown menu
export default function Form(user, idtoken) {
  // The state for the total time input
  const [totalTime, setTotalTime] = useState("");

  // The state for the name of the exercise input
  const [exerciseId, setExercise] = useState("");

  // The state for the sets input
  const [Sets, setSets] = useState([
    { reps: "", weight: "", timeInSeconds: "" },
  ]);

  // The state for the data fetched from the backend api
  const { data, loading } = useFetch("/api/exercises");

  // A handler function to update the total time input
  const handleTotalTimeChange = (e) => {
    setTotalTime(e.target.value);
  };

  // A handler function to update the name of the exercise input
  const handleExerciseChange = (e) => {
    setExercise(e.target.value);
  };

  // A handler function to update the sets input
  const handleSetChange = (index, e) => {
    const newSets = [...Sets];
    newSets[index][e.target.name] = e.target.value;
    setSets(newSets);
  };

  // A handler function to add a new set to the sets input
  const handleAddSet = () => {
    setSets([...Sets, { reps: "", weight: "", timeInSeconds: "" }]);
  };

  // A handler function to remove a set from the sets input
  const handleRemoveSet = (index) => {
    const newSets = [...Sets];
    newSets.splice(index, 1);
    setSets(newSets);
  };
  const gymGoerId = user.user.guid;

  // A handler function to submit the form data
  const handleSubmit = (e) => {
    e.preventDefault();
    
    AddWorkout({ totalTime, gymGoerId, exerciseId, Sets}, user.idtoken)
  };

  return (
    <form onSubmit={handleSubmit}>
      <label htmlFor="totalTime">Total time:</label>
      <input
        id="totalTime"
        type="number"
        value={totalTime}
        onChange={handleTotalTimeChange}
      />
      <label htmlFor="exercise">Name of the exercise:</label>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <Dropdown
          id="exercise"
          data={data}
          value={exerciseId}
          onChange={handleExerciseChange}
        />
      )}
      <label>Sets:</label>
      {Sets.map((set, index) => (
        <div key={index}>
          <Set index={index} values={set} onChange={handleSetChange} />
          <button type="button" onClick={() => handleRemoveSet(index)}>
            Remove
          </button>
        </div>
      ))}
      <button type="button" onClick={handleAddSet}>
        Add
      </button>
      <button type="submit">Submit</button>
    </form>
  );
};
