import './Pagination.css';

export default function Pagination({ currentPage, totalPages, prevPageHandler, nextPageHandler, pageChangeHandler }) {
    return (
        <nav aria-label="Hash Table Pagination">
            <ul className="pagination">
                <li className="page-item">
                    <button 
                        disabled={currentPage === 1}
                        onClick={prevPageHandler}
                        className="btn btn-outline-primary btn-sm"
                    >
                        Previous
                    </button>
                </li>
                <li className="page-item">
                    <label htmlFor="pageNumberInput">Page:</label>
                    <input 
                        id="pageNumberInput"
                        type="number" 
                        min="1" 
                        disabled={totalPages === 1}
                        max={totalPages} 
                        value={currentPage} onChange={(e) => pageChangeHandler(e.target.value)} />
                    <span>of {totalPages.toLocaleString()}</span>
                </li>
                <li className="page-item">
                    <button 
                    disabled={currentPage === totalPages}
                    onClick={nextPageHandler}
                    className="btn btn-outline-primary btn-sm"
                    >
                        Next
                    </button>
                </li>
            </ul>
        </nav>
    )
}