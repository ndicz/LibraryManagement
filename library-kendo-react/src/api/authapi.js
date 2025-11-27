import http from "./httpClient";

export async function login(username, password) {
    const res = await http.post("/Auth/login", { username, password });
    return res.data; // APIResponse<string>
}
