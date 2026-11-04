import { useState, useEffect } from 'react';

function App() {

  const pageSize = 10;
  const [totalHashes, setTotalHashes] = useState(0);
  const [hashes, setHashes] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchKeyword, setSearchKeyword] = useState('');

  const apiUrl = `https://localhost:7281/api`;

  useEffect(() => {
    getHashes(currentPage, pageSize, '');
  }, []);

  function searchHandler() {
    getHashes(1, pageSize, searchKeyword);
    setCurrentPage(1);
  }

  function nextPageHandler() {
    if (currentPage < Math.ceil(totalHashes / pageSize)) {
      const nextPage = currentPage + 1;
      setCurrentPage(nextPage);
      getHashes(nextPage, pageSize, searchKeyword);
    }
  }

  function prevPageHandler() {
    if (currentPage > 1) {
      const prevPage = currentPage - 1;
      setCurrentPage(prevPage);
      getHashes(prevPage, pageSize, searchKeyword);
    }
  }

  function getHashes(page, size, search) {
      fetch(`${apiUrl}/v1/hashes?page=${page}&size=${size}&search=${search}`)
      .then((response) => response.json())
      .then((data) => {
        setHashes(data.hashes);
        setTotalHashes(data.totalResults);
      })
      .catch((error) => console.error('Error fetching hashes:', error));
  }

  return (
    <>
      <h1>Detego Pt.1 Hash Table</h1>
      <p>Total records: {totalHashes.toLocaleString()}</p>
      <div>
        <input type="text" placeholder="Search Hashes..." onChange={(e) => setSearchKeyword(e.target.value)} />
        <button 
          disabled={searchKeyword === ''}
          onClick={searchHandler}>Search</button>
      </div>
      <nav>
        <ul>
          <li>
            <button 
              disabled={currentPage === 1}
              onClick={prevPageHandler}>
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
            <span> of {Math.ceil(totalHashes / pageSize).toLocaleString()}</span>
          </li>
          <li>
            <button 
              disabled={currentPage === Math.ceil(totalHashes / pageSize)}
              onClick={nextPageHandler}>
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
