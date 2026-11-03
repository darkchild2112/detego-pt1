import { useState, useEffect } from 'react';

function App() {

  const [hashes, setHashes] = useState([]);

  useEffect(() => {
    fetch('https://localhost:7281/api/v1/hashes')
      .then((response) => response.json())
      .then((data) => setHashes(data))
      .catch((error) => console.error('Error fetching hashes:', error));
  }, []);

  return (
    <>
      <h1>Detego Pt.1 Hash Table</h1>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Hash</th>
          </tr>
        </thead>
        <tbody>
          {hashes.map((hash) => (
            <tr key={hash.id}>
              <td>{hash.id}</td>
              <td>{hash.code}</td>
            </tr>
          ))}
        </tbody>      
      </table>
    </>
  );
}

export default App;
