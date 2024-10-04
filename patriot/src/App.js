import './App.css';
import LoginPage from "./pages/LoginPage";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUpPage from "./pages/SignUpPage";
import AccountPage from "./pages/AccountPage";
import FeedPage from "./pages/FeedPage";
import HomePage from "./pages/HomePage";
import CreatorCard from "./components/CreatorCard";
import CreatorPage from "./pages/CreatorPage";

function App() {
  return (
      <Router>
          <Routes>
              <Route path={"/"} element={<HomePage/>} />
              <Route path={"/login"} element={<LoginPage/>}/>
              <Route path={"/signUp"} element={<SignUpPage/>}/>
              <Route path={"/feed"} element={<FeedPage/>}/>
              <Route path={"/account"} element={<AccountPage/>}/>
              <Route path={"/creator/:id"} element={<CreatorPage/>}/>
          </Routes>
      </Router>

  );
}

export default App;
