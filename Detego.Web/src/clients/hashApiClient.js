const apiUrl = `https://localhost:7281/api`;
  
export function getHashes(page, size, search) {
    return fetch(`${apiUrl}/v1/hashes?page=${page}&size=${size}&search=${encodeURIComponent(search)}`)
        .then((response) => { 

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return response.json() 
        })
}