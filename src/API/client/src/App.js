import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/RegisterPage";
import WelcomePage from "./pages/WelcomePage";
import Header from "./components/HeaderAuthed";
import { BrowserRouter, Routes, Route} from 'react-router-dom';

import { ChakraProvider } from "@chakra-ui/react";
import './index.css';


function App() {
  return (
    <ChakraProvider>
        <BrowserRouter>
            <Routes>
                <Route path={"/"} exact element={<HomePage/>} />
                <Route path={"/login"} element={<LoginPage/>} />
                <Route path={"/register"} element={<RegisterPage/>}/>
                <Route path={"/welcome"} element={<WelcomePage/>}/>
            </Routes>
        </BrowserRouter>
    </ChakraProvider>
  );
}

export default App;
