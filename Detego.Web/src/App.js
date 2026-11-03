import { useState, useEffect } from 'react';

function App() {

  const pageSize = 1000;
  const [totalHashes, setTotalHashes] = useState(0);
  const [hashes, setHashes] = useState([]);

  useEffect(() => {
    fetch(`https://localhost:7281/api/v1/hashes?page=1&size=${pageSize}`)
      .then((response) => response.json())
      .then((data) => {
        setHashes(data.hashes);
        setTotalHashes(data.totalResults);
      })
      .catch((error) => console.error('Error fetching hashes:', error));
  }, []);

  return (
    <>
      <h1>Detego Pt.1 Hash Table</h1>
      <p>Total hashes: {totalHashes}</p>

      <nav aria-label="Page navigation example">
        <ul class="pagination">
          <li class="page-item"><a class="page-link" href="#">Previous</a></li>
            {Array.from({ length: Math.ceil(totalHashes / pageSize) }, (_, index) => (
              <li key={index} class="page-item"><a class="page-link" href="#">{index + 1}</a></li>
            ))}
          <li class="page-item"><a class="page-link" href="#">Next</a></li>
        </ul>
      </nav>
                  <hr />
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
