import http from "./httpClient";

export async function getBooks() {
    const res = await http.get("/Books");
    return res.data;
}

export async function createBook(dto) {
    const res = await http.post("/Books", dto);
    return res.data;
}

export async function updateBook(id, dto) {
    const res = await http.put(`/Books/${id}`, dto);
    return res.data;
}

export async function deleteBook(id) {
    const res = await http.delete(`/Books/${id}`);
    return res.data;
}
