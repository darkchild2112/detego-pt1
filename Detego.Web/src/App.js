import { useState, useEffect } from 'react';

import Search from './components/Search/Search';
import HashTable from './components/HashTable/HashTable';
import Pagination from './components/Pagination/Pagination';

import { getHashes } from './clients/hashApiClient';

function App() {

  const pageSize = 10;
  const [totalHashes, setTotalHashes] = useState(0);
  const [hashes, setHashes] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchKeyword, setSearchKeyword] = useState('');
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    getHashesHandler(currentPage, pageSize, '');
  }, []);

  function searchHandler() {
    setCurrentPage(1);
    getHashesHandler(1, pageSize, searchKeyword);
  }

  function nextPageHandler() {
    if (currentPage < Math.ceil(totalHashes / pageSize)) {
      const nextPage = currentPage + 1;
      setCurrentPage(nextPage);
      getHashesHandler(nextPage, pageSize, searchKeyword);
    }
  }

  function prevPageHandler() {
    if (currentPage > 1) {
      const prevPage = currentPage - 1;
      setCurrentPage(prevPage);
      getHashesHandler(prevPage, pageSize, searchKeyword);
    }
  }

  function pageChangeHandler(value) {
    if (value < 1 || value > totalPages) return;
    const newPage = parseInt(value) || 1; 
    setCurrentPage(newPage); 
    getHashesHandler(newPage, pageSize, searchKeyword);
  }

  function getHashesHandler(page, size, search) {
      getHashes(page, size, search)
        .then((data) => {
          setHashes(data.hashes);
          setTotalHashes(data.totalResults);
          setTotalPages(data.totalPages);
        })
        .catch((error) => console.error('Error fetching hashes:', error));
  }

  return (
    <main className="container mt-4">
      <h1>Detego Pt.1 Hash Table</h1>
      <p>Total records: {totalHashes.toLocaleString()}</p>
      <Search 
        searchKeyword={searchKeyword} 
        keywordChangeHandler={setSearchKeyword} 
        searchHandler={searchHandler} 
      />

      <HashTable hashes={hashes} />

      <Pagination 
        currentPage={currentPage} 
        totalPages={totalPages}
        prevPageHandler={prevPageHandler}
        nextPageHandler={nextPageHandler}
        pageChangeHandler={pageChangeHandler}
      /> 
    </main>
  );
}

export default App;
