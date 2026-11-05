const apiUrl = `https://localhost:7281/api`;
  
export function getHashes(page, size, search) {
    return fetch(`${apiUrl}/v1/hashes?page=${page}&size=${size}&search=${search}`)
        .then((response) => response.json())
}