export default function Search({ searchKeyword, keywordChangeHandler, searchHandler }) {
  return (
    <div className="row g-3">
        <div className="col-auto">
          <label 
            htmlFor="searchInput" 
            className="visually-hidden"
          >
            Search Hashes:
          </label>
          <input 
            type="text" 
            id="searchInput" 
            placeholder="Search Hashes..." 
            className="form-control"
            onChange={(e) => keywordChangeHandler(e.target.value)} 
          />
        </div>
        <div className="col-auto">
          <button 
            onClick={() => searchHandler()}
            className="btn btn-outline-primary"
          >
              Search
          </button>
        </div>
    </div>
    )
}