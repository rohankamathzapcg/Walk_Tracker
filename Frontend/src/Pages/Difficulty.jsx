// eslint-disable-next-line no-unused-vars
import React, { useEffect, useRef, useState } from 'react';
import axios from "axios";
import { ToastContainer, toast } from 'react-toastify';

const Difficulty = () => {

  const [difficulties, setDifficulties] = useState([])
  const [difficultyDetails, setDifficultyDetails] = useState({
    name: "",
  })
  const closeRef = useRef(null)

  useEffect(() => {
    axios.get("https://localhost:7258/api/Difficulty")
      .then((result) => {
        setDifficulties(result.data)
      })
      .catch((err) => console.log("Error while fetching the data " + err))
  }, [])

  const handleCloseBtn = () => {
    if (closeRef.current) {
      closeRef.current.click();
    }
  }

  const handleSubmit = () => {
    axios.post("https://localhost:7258/api/Difficulty", difficultyDetails)
      .then((result) => {
        if (result.status === 200) {
          toast.success("New Difficulty Added Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          setDifficultyDetails({
            name: "",
          });
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);
        }
      })
      .catch(err => console.log(err))
  }

  return (
    <>
      <ToastContainer />
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Difficulties</h2>
        <button type="button" className="btn btn-sm btn-lg btn-success mb-3" data-bs-toggle="modal" data-bs-target="#difficultyModal">
          Add Difficulty Level
        </button>

        {/* Modal */}
        <div className="modal fade" id="difficultyModal" tabIndex={-1} aria-labelledby="regionModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="regionModalLabel">Add Difficulty Level</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <div className="input-group mb-3">
                  <span className="input-group-text">Difficulty Level</span>
                  <input type="text" value={difficultyDetails.name} onChange={(e) => setDifficultyDetails({ ...difficultyDetails, name: e.target.value })} className="form-control shadow-none" aria-label="Difficulty Level" />
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                <button type="button" className="btn btn-primary shadow-none" onClick={handleSubmit}>Add Difficulty</button>
              </div>
            </div>
          </div>
        </div>

        {
          difficulties.length <= 0 ? <p className='text Center'>No Records Found!</p> :
            (
              <div className="table-responsive">
                <table className="table table-striped table-bordered">
                  <thead>
                    <tr>
                      <th scope="col" className='text-center'>Difficulty Id</th>
                      <th scope="col" className='text-center'>Difficulty Level</th>
                      <th scope="col" className='text-center'>Actions</th>
                    </tr>
                  </thead>
                  <tbody className="table-group-divider">
                    {
                      difficulties.map((difficulty, index) => {
                        return (
                          <tr key={index}>
                            <th scope="row" className='text-center'>{difficulty.id}</th>
                            <td className='text-center'>{difficulty.name}</td>
                            <td className='text-center'>
                              <button className='btn btn-warning'>Edit</button>
                            </td>
                          </tr>
                        )
                      })
                    }
                  </tbody>
                </table>
              </div>
            )
        }
      </div>
    </>
  );
}

export default Difficulty;
