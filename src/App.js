import "./App.css";
import { useState } from "react";
import axios from "axios";
import pic from "./source/download.png";

function App() {
  let [input, setInput] = useState("");
  let [list, setlist] = useState([]);

  const getExamples = async () => {
    let res = await axios.post("http://localhost:8080/", { word: input });
    if (res.data.length > 0) {
      setlist(res.data);
    } else {
      alert("The word " + input + " does not exist");
    }
  };

  const handleChange = (event) => {
    setInput(event.target.value);
  };
  return (
    <div className="App">
      <h1>Search Word</h1>
      <div  >
        <input style={{margin:"20px"}}
          type="text"
          placeholder="enter name to search..."
          onChange={handleChange}
        />
        
          <img className="img"
            style={{ width: "200px", height: "110px", cursor: "pointer", position:"relative", top:"40px"}}
            src={pic}
            alt="no pic"
            onClick={getExamples}
          />
        
      </div>

      <ul style={{ width: "25%", position: "relative", left: "35%"}}>
        {list &&
          list.map((item) => (
            <li key={item}>
              {<div dangerouslySetInnerHTML={{ __html: item }} />}
            </li>
          ))}
      </ul>
    </div>
  );
}

export default App;
