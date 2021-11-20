import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";

import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';

function App() {
  return (
    <div>
        <BrowserRouter>
            <Routes>
                <Route path={"/"} element={<HomePage/>} />
                <Route path={"/login"} element={<LoginPage/>} />
            </Routes>
        </BrowserRouter>
    </div>
  );
}

export default App;
