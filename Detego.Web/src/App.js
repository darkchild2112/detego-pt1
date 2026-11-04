import { useState, useEffect } from 'react';

function App() {

  const pageSize = 10;
  const [totalHashes, setTotalHashes] = useState(0);
  const [hashes, setHashes] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    fetch(`https://localhost:7281/api/v1/hashes?page=${currentPage}&size=${pageSize}`)
      .then((response) => response.json())
      .then((data) => {
        setHashes(data.hashes);
        setTotalHashes(data.totalResults);
      })
      .catch((error) => console.error('Error fetching hashes:', error));
  }, [currentPage]);

  return (
    <>
      <h1>Detego Pt.1 Hash Table</h1>
      <p>Total hashes: {totalHashes}</p>

      <nav aria-label="Page navigation example">
        <ul>
          <li>
            <button 
              disabled={currentPage === 1}
              onClick={(e) => { e.preventDefault(); setCurrentPage((prev) => Math.max(1, prev - 1)); }}>
                Previous
            </button>
          </li>
          <li>
            <span>Page: </span>
            <input 
              type="number" 
              min="1" 
              max={Math.ceil(totalHashes / pageSize)} 
              value={currentPage} onChange={(e) => setCurrentPage(parseInt(e.target.value) || 1)} />
            <span> of {Math.ceil(totalHashes / pageSize)}</span>
          </li>
          <li>
            <button 
              disabled={currentPage === Math.ceil(totalHashes / pageSize)}
              onClick={(e) => { e.preventDefault(); setCurrentPage((prev) => Math.min(Math.ceil(totalHashes / pageSize), prev + 1)); }}>
                Next
            </button>
          </li>
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
