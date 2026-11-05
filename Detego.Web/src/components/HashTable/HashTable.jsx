export default function HashTable({ hashes }) {
    return (
        <table className="table table-striped">
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
    );
}
