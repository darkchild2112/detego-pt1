export default function Pagination({ currentPage, totalPages, prevPageHandler, nextPageHandler, pageChangeHandler }) {
    return (
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
                    disabled={totalPages === 1}
                    max={totalPages} 
                    value={currentPage} onChange={(e) => pageChangeHandler(e.target.value)} />
                    <span> of {totalPages.toLocaleString()}</span>
                </li>
                <li>
                    <button 
                    disabled={currentPage === totalPages}
                    onClick={nextPageHandler}>
                        Next
                    </button>
                </li>
            </ul>
        </nav>
    )
}