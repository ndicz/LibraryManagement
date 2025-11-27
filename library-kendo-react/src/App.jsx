import { useState } from "react";
import LoginForm from "./component/auth/LoginForm.jsx";
import { login } from "./api/authapi.js";
import { setAuthToken } from "./api/httpclient.js";
import { parseJwt } from "./utils/jwt.js";

function App() {
    const [token, setToken] = useState("");
    const [payload, setPayload] = useState(null);

    const handleLogin = async (username, password) => {
        const res = await login(username, password);
        if (!res.success) throw new Error(res.message || "Login gagal");
        const t = res.data;
        setToken(t);
        setAuthToken(t);
        setPayload(parseJwt(t));
    };

    const handleLogout = () => {
        setToken("");
        setPayload(null);
        setAuthToken(null);
    };

    if (!token) {
        return <LoginForm onLogin={handleLogin} />;
    }

    return (
        <div style={{ padding: 20 }}>
            <h1>Library Frontend</h1>
            <p>Login OK. Token sudah disimpan.</p>
            <button onClick={handleLogout}>Logout</button>
            <pre>{JSON.stringify(payload, null, 2)}</pre>
        </div>
    );
}

export default App;
