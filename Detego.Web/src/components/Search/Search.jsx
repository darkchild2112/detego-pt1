export default function Search({ searchKeyword, keywordChangeHandler, searchHandler }) {
  return (
    <div>
        <input type="text" placeholder="Search Hashes..." onChange={(e) => keywordChangeHandler(e.target.value)} />
        <button 
          disabled={searchKeyword === ''}
          onClick={() => searchHandler()}>
            Search
        </button>
    </div>
    )
}