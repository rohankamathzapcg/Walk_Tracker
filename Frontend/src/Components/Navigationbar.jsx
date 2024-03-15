import { Link } from "react-router-dom";

const Navigationbar = () => {
  return (
    <>
      <nav className="navbar navbar-expand-lg bg-dark p-4 sticky-top" data-bs-theme="dark">
        <div className="container-fluid">
          <Link className="navbar-brand" to="/">Walk Tracker</Link>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item">
                <Link className="nav-link mx-3" aria-current="page" to="/">Home</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link mx-3" to="/regions">Regions</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link mx-3" to="/difficulty">Difficulty</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link mx-3" to="/walks">Walks</Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </>
  );
}

export default Navigationbar;
