// eslint-disable-next-line no-unused-vars
import React, { useRef } from 'react'
import { useEffect, useState } from 'react'
import axios from 'axios'
import { ToastContainer, toast } from 'react-toastify';

const Walk = () => {
  const [walkDetails, setWalkDetails] = useState([]);
  const [regions, setRegions] = useState([]);
  const [difficulties, setDifficulties] = useState([]);
  const [walkData, setWalkData] = useState({
    name: "",
    description: "",
    distance: 0,
    imageURL: "",
    difficultyId: 0,
    regionId: 0
  })
  const [editWalkData, setEditWalkData] = useState({
    name: "",
    description: "",
    distance: 0,
    imageURL: "",
    difficultyId: 0,
    regionId: 0
  })
  const [selectedId, setSelectedId] = useState(null)
  const [isSmallScreen, setIsSmallScreen] = useState(window.innerWidth < 768);
  const closeRef = useRef(null);

  useEffect(() => {
    //Getting All Walks
    axios.get("https://localhost:7258/api/Walk")
      .then((result) => {
        setWalkDetails(result.data)
      })
      .catch((err) => console.log("Error while fetching data " + err))

    //Getting All Regions
    axios.get("https://localhost:7258/api/Regions")
      .then((result) => {
        setRegions(result.data)
      })
      .catch((err) => console.log("Error while fetching data " + err))

    //Getting All Difficulties
    axios.get("https://localhost:7258/api/Difficulty")
      .then((result) => {
        setDifficulties(result.data)
      })
      .catch((err) => console.log("Error while fetching data " + err))

    const handleResize = () => {
      setIsSmallScreen(window.innerWidth < 768);
    }

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    }

  }, [])

  const handleCloseBtn = () => {
    if (closeRef.current) {
      closeRef.current.click();
    }
  }

  const handleEdit = (id) => {
    setSelectedId(id)
    for (let i = 0; i < walkDetails.length; i++) {
      if (walkDetails[i].id === id) {
        setEditWalkData({
          name: walkDetails[i].name,
          description: walkDetails[i].description,
          distance: walkDetails[i].distance,
          imageURL: walkDetails[i].imageURL,
          difficultyId: walkDetails[i].difficultyId,
          regionId: walkDetails[i].regionId
        })
      }
    }
  }

  const handleDelete = (id) => {
    setSelectedId(id)
  }

  const handleRemove = () => {
    axios.delete(`https://localhost:7258/api/Walk/${selectedId}`)
      .then((result) => {
        if (result.status === 200) {
          toast.success("Walk deleted Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);
          console.log(regions)
        } else {
          toast.error("Something went wrong!!", {
            theme: "dark",
            autoClose: 1000,
          });
        }
      })
      .catch(err => console.log(err))
  }

  const handleUpdate = () => {
    axios.put(`https://localhost:7258/api/Walk/${selectedId}`, editWalkData)
      .then((result) => {
        if (result.status === 200) {
          toast.success("Walk updated Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);

        } else {
          toast.error("Invalid Data", {
            theme: "dark",
            autoClose: 1000,
          });
        }
      })
      .catch(() => toast.error("Something went wrong! Try Again Later", {
        theme: "dark",
        autoClose: 1000,
      }))
  }

  const handleSubmit = () => {
    axios.post("https://localhost:7258/api/Walk", walkData)
      .then((result) => {
        if (result.status === 200) {
          toast.success("New Walk Added Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          setWalkData({
            name: "",
            description: "",
            distance: 0,
            imageURL: "",
            difficultyId: 0,
            regionId: 0
          })
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);
        } else {
          toast.error("Invalid Data", {
            theme: "dark",
            autoClose: 1000,
          });
        }
      })
      .catch(err => console.log(err))
  }

  return (
    <>
      <ToastContainer />
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Walking Details</h2>
        <button type="button" className="btn btn-sm btn-lg btn-success mb-3" data-bs-toggle="modal" data-bs-target="#walkModal">Add New Walk</button>

        {/* Add Walk Modal */}
        <div className="modal fade" id="walkModal" tabIndex={-1} aria-labelledby="walkModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="walkModalLabel">Add New Walks</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <div className="input-group mb-3">
                  <span className="input-group-text">Place Name</span>
                  <input type="text" className="form-control shadow-none" aria-label="Place Name" value={walkData.name} onChange={(e) => setWalkData({ ...walkData, name: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Place Description</span>
                  <input type="text" className="form-control shadow-none" aria-label="Place Description" value={walkData.description} onChange={(e) => setWalkData({ ...walkData, description: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Distance (Km)</span>
                  <input type="number" className="form-control shadow-none" aria-label="Distance" value={walkData.distance} onChange={(e) => setWalkData({ ...walkData, distance: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <input type="file" className="form-control shadow-none" aria-label="Place Image" onChange={(e) => setWalkData({ ...walkData, imageURL: e.target.files[0].name })} />
                </div>
                <div className="input-group mb-3">
                  <label className="input-group-text" htmlFor="difficultyID">Difficulty Level</label>
                  <select className="form-select" id="difficultyID" value={walkData.difficultyId} onChange={(e) => setWalkData({ ...walkData, difficultyId: e.target.value })}>
                    <option value={0}>Select</option>
                    {
                      difficulties.map((difficulty, index) => {
                        return (
                          <option key={index} value={difficulty.id}>{difficulty.name}</option>
                        )
                      })
                    }
                  </select>
                </div>
                <div className="input-group mb-3">
                  <label className="input-group-text" htmlFor="regionID">Region</label>
                  <select className="form-select" id="regionID" value={walkData.regionId} onChange={(e) => setWalkData({ ...walkData, regionId: e.target.value })}>
                    <option value={0}>Select</option>
                    {
                      regions.map((region, index) => {
                        return (
                          <option key={index} value={region.id}>{region.name}</option>
                        )
                      })
                    }
                  </select>
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                <button type="button" onClick={handleSubmit} className="btn btn-primary shadow-none">Add Walk</button>
              </div>
            </div>
          </div>
        </div>

        {/* Edit Walk Modal */}
        <div className="modal fade" id="editWalkModal" tabIndex={-1} aria-labelledby="editWalkModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="editWalkModalLabel">Add New Walks</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <div className="input-group mb-3">
                  <span className="input-group-text">Place Name</span>
                  <input type="text" className="form-control shadow-none" aria-label="Place Name" value={editWalkData.name} onChange={(e) => setEditWalkData({ ...editWalkData, name: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Place Description</span>
                  <input type="text" className="form-control shadow-none" aria-label="Place Description" value={editWalkData.description} onChange={(e) => setEditWalkData({ ...editWalkData, description: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Distance (Km)</span>
                  <input type="number" className="form-control shadow-none" aria-label="Distance" value={editWalkData.distance} onChange={(e) => setEditWalkData({ ...editWalkData, distance: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <input type="file" className="form-control shadow-none" aria-label="Place Image" onChange={(e) => setEditWalkData({ ...editWalkData, imageURL: e.target.files[0].name })} />
                </div>
                <div className="input-group mb-3">
                  <label className="input-group-text" htmlFor="difficultyID">Difficulty Level</label>
                  <select className="form-select" id="difficultyID" value={editWalkData.difficultyId} onChange={(e) => setEditWalkData({ ...editWalkData, difficultyId: e.target.value })}>
                    <option value={0}>Select</option>
                    {
                      difficulties.map((difficulty, index) => {
                        return (
                          <option key={index} value={difficulty.id}>{difficulty.name}</option>
                        )
                      })
                    }
                  </select>
                </div>
                <div className="input-group mb-3">
                  <label className="input-group-text" htmlFor="regionID">Region</label>
                  <select className="form-select" id="regionID" value={editWalkData.regionId} onChange={(e) => setEditWalkData({ ...editWalkData, regionId: e.target.value })}>
                    <option value={0}>Select</option>
                    {
                      regions.map((region, index) => {
                        return (
                          <option key={index} value={region.id}>{region.name}</option>
                        )
                      })
                    }
                  </select>
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                <button type="button" onClick={handleUpdate} className="btn btn-primary shadow-none">Update Walk</button>
              </div>
            </div>
          </div>
        </div>

        {/* Delete Modal */}
        <div className="modal fade" id="deleteWalkModal" tabIndex={-1} aria-labelledby="deleteWalkModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="deleteWalkModalLabel">Delete Region</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <p>Do you want to delete this specific walk details ?</p>
                <div className="modal-footer">
                  <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                  <button type="button" onClick={handleRemove} className="btn btn-danger shadow-none">Delete Region</button>
                </div>
              </div>
            </div>
          </div>
        </div>


        {
          walkDetails.length <= 0 ? <p className='text-center'>No records found</p> :
            (
              <div className="table-responsive">
                <table className="table table-striped table-bordered">
                  <thead>
                    <tr>
                      <th scope="col" className='text-center'>Place Name</th>
                      <th scope="col" className='text-center'>Description</th>
                      <th scope="col" className='text-center'>Distance (Km)</th>
                      <th scope="col" className='text-center'>Image</th>
                      <th scope="col" className='text-center'>Region Code</th>
                      <th scope="col" className='text-center'>Region Name</th>
                      <th scope="col" className='text-center'>Difficulty Level</th>
                      <th scope="col" className='text-center'>Actions</th>
                    </tr>
                  </thead>
                  <tbody className="table-group-divider">
                    {walkDetails.map((walk, index) => {
                      return (
                        <tr key={index}>
                          <th className='text-center'>{walk.name}</th>
                          <td className='text-center'>{walk.description}</td>
                          <td className='text-center'>{walk.distance}</td>
                          <td className='text-center'>{walk.imageURL === "" ? "No Image" : walk.imageURL}</td>
                          <td className='text-center'>{walk.region.code}</td>
                          <td className='text-center'>{walk.region.name}</td>
                          <td className='text-center'>{walk.difficulty.name}</td>
                          <td>
                            <div className={`d-flex justify-content-${isSmallScreen ? 'start' : 'center'}`}>
                              <button type="button" onClick={() => handleEdit(walk.id)} className="btn btn-warning me-2" style={{ width: '80px' }} data-bs-toggle="modal" data-bs-target="#editWalkModal">Edit</button>
                              <button className="btn btn-danger" onClick={() => handleDelete(walk.id)} style={{ width: '80px' }} data-bs-toggle="modal" data-bs-target="#deleteWalkModal">Delete</button>
                            </div>
                          </td>
                        </tr>
                      )
                    })}
                  </tbody>
                </table>
              </div>
            )
        }
      </div>
    </>
  )
}

export default Walk
