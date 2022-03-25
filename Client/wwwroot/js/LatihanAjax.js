$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    console.log(result)
    console.log(result.results);
    var test = "";
    $.each(result.results, function (key, val) {
        test += `  <tr>
                            <td>${key + 1}</td>
                            <td class="text-capitalize">${val.name}</td>
                            <td><button class="btn btn-primary modal-detail-button" data-toggle="modal" data-target="#detailModal" onclick="showDetail('${val.url}')">Detail</button></td>
`;
    })
/*    console.log(test);*/
    $('#detailPoke').html(test);


}).fail((err) => {
    console.log(err);
})


function showDetail(url) {
    fetch(url).then(function (response) {
        response.json().then(function (pokemon) {
            document.getElementById('modal').innerHTML = ''

            document.getElementById('modal').insertAdjacentHTML('beforeend',
                `<div class="img-container">
                    <img id="update_img" src="${pokemon.sprites.other.dream_world.front_default}" alt="">
                 </div>
                  <div class="detail-container">
                      <div class="title-container">
                          <h3 class="name text-center" id="update_name">${pokemon.name}</h3>
                              <div class="stats text-center">
                                    <div class="progress">
                                       <div class="progress-bar bg-success" role="progressbar" style="width:  ${pokemon.stats[0].base_stat}%" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100"></div>
                                       <div class="progress-bar" role="progressbar" style="width: ${pokemon.base_experience}%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100"></div>
                                       <div class="progress-bar bg-warning" role="progressbar" style="width: ${pokemon.base_experience}%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div> <br />
                                       <span class="first cp-text col-md" id="update_hp">HP : ${pokemon.stats[0].base_stat}</span>
                                       <span class="cp-text col-md-6" id="update_cp">XP ${pokemon.base_experience}</span>
                                       <span class="cp-text col-md-6" id="update_att">Attack ${pokemon.stats[1].base_stat}</span>
                              </div>
                      </div>   <br />
                  <div class="attributes-container">
                      <div class="col attributes-content" style="min-width: 42%;">
                          <p class="cp-text" id="update_type">${pokemon.types[0].type.name}</p>
                          <small class="text-muted">Type</small>
                      </div>
                  <div class="col attributes-content">
                        <p class="cp-text" id="update_weight">${pokemon.weight}</p>
                        <small class="text-muted">Weight</small>
                  </div>
                      <div class="col attributes-content">
                        <p class="cp-text no-border" id="update_height">${pokemon.height}</p>
                        <small class="text-muted">Height</small>
                      </div>
                  </div>
                  <div class="player-data">
                      <div class="col data-container">
                      <p class="stardust text-capitalize" id="update_move">${pokemon.moves[0].move.name}</p>
                      <p class="muted-text">Move</p>
                      </div>
                      <div class="col data-container">
                      <p class="stardust text-capitalize" id="update_ability">${pokemon.abilities[0].ability.name}</p>
                      <p class="muted-text" id="update_ability_title">Ability</p>
                      </div>
                  </div>
                   
                `
            )
        })
    })
   
}
